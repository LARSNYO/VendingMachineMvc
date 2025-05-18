using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using testmvcapp.Services.Interfaces;
using testmvcapp.ViewModels;

namespace testmvcapp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IDrinkService _drinkService;

        public CartController(ICartService cartItemService, IDrinkService drinkService)
        {
            _cartService = cartItemService;
            _drinkService = drinkService;
        }

        [AllowAnonymous]
        // GET: Drinks
        public async Task<IActionResult> Index()
        {
            var cartItems = await _cartService.GetCartItemsAsync();
            var cartTotal = await _cartService.GetCartTotalAsync();

            var viewModel = new CartViewModel
            {
                CartItems = cartItems.ToList(),
                Total = cartTotal
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var drink = await _drinkService.GetDrinkByIdAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            await _cartService.AddToCartAsync(drink, 1);

            return RedirectToAction("Index", "Catalog");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var drink = await _drinkService.GetDrinkByIdAsync(id);
            if (drink != null)
            {
                await _cartService.RemoveFromCartAsync(drink);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCartInCatalog(int id)
        {
            var drink = await _drinkService.GetDrinkByIdAsync(id);

            if (drink != null)
            {
                await _cartService.RemoveFromCartAsync(drink);
            }

            return RedirectToAction("Index", "Catalog");
        }

        public async Task<IActionResult> ReduceQuantityAsync(int id)
        {
            var drink = await _drinkService.GetDrinkByIdAsync(id);

            if (drink != null)
            {
                await _cartService.ReduceQuantityAsync(drink);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> IncreaseQuantityAsync(int id)
        {
            var drink = await _drinkService.GetDrinkByIdAsync(id);

            if (drink != null)
            {
                await _cartService.IncreaseQuantityAsync(drink);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCart();

            return RedirectToAction("Index");
        }
    }
}