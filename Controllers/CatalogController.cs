using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using testmvcapp.ViewModels;
using testmvcapp.Services.Interfaces;

namespace testmvcapp.Controllers;

public class CatalogController : Controller
{
    private readonly IDrinkService _drinkService;
    private readonly IBrandService _brandService;
    private readonly ICartService _cartService;
    public CatalogController(IDrinkService drinkService, IBrandService brandService, ICartService cartService)
    {
        _drinkService = drinkService;
        _brandService = brandService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index(string selectedBrandId, string minPrice)
    {
        int? brandId = !string.IsNullOrEmpty(selectedBrandId) ? int.Parse(selectedBrandId) : null;
        int? min = !string.IsNullOrEmpty(minPrice) ? int.Parse(minPrice) : null;

        var drinks = await _drinkService.GetFilterAsync(brandId, min);

        var minAvailable = drinks.Any() ? drinks.Min(d => d.Price) : 0;
        var maxAvailable = drinks.Any() ? drinks.Max(d => d.Price) : 0;

        var brands = await _brandService.GetBrandsAsync();
        var selectList = new SelectList(brands, "Id", "Name", brandId);

        var cartItems = await _cartService.GetCartItemsAsync();
        var selectedCount = cartItems.Count();
        var SelectedDrinkIds = cartItems.Select(ci => ci.Drink.Id).ToList();

        var viewModel = new DrinkFilterViewModel
        {
            Drinks = drinks.ToList(),
            Brands = selectList,
            SelectedBrandId = brandId,
            MinPrice = minPrice,
            MinAvailablePrice = minAvailable,
            MaxAvailablePrice = maxAvailable,
            SelectedCount = selectedCount,
            SelectedDrinkIds = SelectedDrinkIds
        };

        return View(viewModel);
    }
}