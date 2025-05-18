using testmvcapp.Services.Interfaces;
using testmvcapp.Repositories.Interfaces;
using testmvcapp.Models;

namespace testmvcapp.Services;
public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _cartId;

    public CartService(ICartRepository cartRepository, IHttpContextAccessor httpContextAccessor)
    {
        _cartRepository = cartRepository;
        _httpContextAccessor = httpContextAccessor;

        _cartId = GetCartId();
    }

    public string GetCartId()
    {
        var session = _httpContextAccessor.HttpContext!.Session;
        string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();
        session.SetString("Id", cartId);
        return cartId;
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
    {
        return await _cartRepository.GetCartItemsAsync(_cartId);
    }

    public async Task<int> GetCartTotalAsync()
    {
        return await _cartRepository.GetCartTotalAsync(_cartId);
    }

    public async Task AddToCartAsync(Drink drink, int quantity)
    {
        var cartItem = await _cartRepository.GetCartItemByIdAsync(_cartId, drink.Id);
        int totalIncart = cartItem?.Quantity ?? 0;

        if (totalIncart + quantity > drink.Quantity) return;

        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                CartId = _cartId,
                Drink = drink,
                Quantity = quantity
            };
            await _cartRepository.AddCartItemAsync(cartItem);
        }
        else
        {
            cartItem.Quantity += quantity;
            _cartRepository.UpdateCartItem(cartItem);
        }

        await _cartRepository.SaveCartItemChangesAsync();
    }

    public async Task RemoveFromCartAsync(Drink drink)
    {
        var cartItem = await _cartRepository.GetCartItemByIdAsync(_cartId, drink.Id);
        if (cartItem != null)
        {
            _cartRepository.RemoveCartItem(cartItem);
            await _cartRepository.SaveCartItemChangesAsync();
        }
    }

    public async Task<int> IncreaseQuantityAsync(Drink drink)
    {
        var cartItem = await _cartRepository.GetCartItemByIdAsync(_cartId, drink.Id);
        if (cartItem != null && cartItem.Quantity < drink.Quantity)
        {
            cartItem.Quantity++;
            _cartRepository.UpdateCartItem(cartItem);
            await _cartRepository.SaveCartItemChangesAsync();
            return cartItem.Quantity;
        }

        return cartItem?.Quantity ?? 0;
    }

    public async Task<int> ReduceQuantityAsync(Drink drink)
    {
        var cartItem = await _cartRepository.GetCartItemByIdAsync(_cartId, drink.Id);
        if (cartItem != null)
        {
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                _cartRepository.UpdateCartItem(cartItem);
            }

            await _cartRepository.SaveCartItemChangesAsync();
            return cartItem.Quantity;
        }

        return 0;
    }

    public async Task ClearCart()
    {
        _cartRepository.ClearCart(_cartId);
        await _cartRepository.SaveCartItemChangesAsync();
    }
}
