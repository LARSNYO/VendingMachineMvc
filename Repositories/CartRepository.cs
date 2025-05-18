using testmvcapp.Data;
using testmvcapp.Repositories.Interfaces;
using testmvcapp.Models;
using Microsoft.EntityFrameworkCore;

namespace testmvcapp.Repositories;

public class CartRepository : ICartRepository
{
    private readonly TestDbContext _context;
    public CartRepository(TestDbContext context)
    {
        _context = context;
    }

    public async Task<CartItem?> GetCartItemByIdAsync(string cartId, int drinkId)
    {
        return await _context.CartItems.Include(ci => ci.Drink).FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.Drink.Id == drinkId);
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string cartId)
    {
        return await _context.CartItems.Where(ci => ci.CartId == cartId).Include(ci => ci.Drink).ToListAsync();
    }

    public async Task<int> GetCartTotalAsync(string cartId)
    {
        return await _context.CartItems.Where(ci => ci.CartId == cartId).SumAsync(ci => ci.Drink.Price * ci.Quantity);
    }

    public async Task AddCartItemAsync(CartItem cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
    }

    public void UpdateCartItem(CartItem cartItem)
    {
        _context.CartItems.Update(cartItem);
    }

    public void RemoveCartItem(CartItem cartItem)
    {
        _context.CartItems.Remove(cartItem);
    }

    public void ClearCart(string cartId)
    {
        var cartItems = _context.CartItems.Where(ci => ci.CartId == cartId);
        _context.CartItems.RemoveRange(cartItems);
    }

    public async Task SaveCartItemChangesAsync()
    {
        await _context.SaveChangesAsync();
    }


    // public async Task<bool> CartItemExistsAsync(string cartId)
    // {
    //     return await _context.CartItems.AnyAsync(ci => ci.CartId == cartId);
    // }
}