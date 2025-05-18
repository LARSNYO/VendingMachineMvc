using Microsoft.AspNetCore.Mvc;
using testmvcapp.Models;
using testmvcapp.ViewModels;
using testmvcapp.Services.Interfaces;

namespace testmvcapp.Controllers;
public class PaymentController : Controller
{
    private readonly ICartService _cartService;
    private readonly ICoinService _coinService;
    public PaymentController(ICartService cartItemService, ICoinService coinService)
    {
        _cartService = cartItemService;
        _coinService = coinService;
    }

    public async Task<IActionResult> Index()
    {
        var totalAmount = await _cartService.GetCartTotalAsync();

        var viewModel = new PaymentViewModel
        {
            TotalAmount = totalAmount,
            Denominations = (await _coinService.GetCoinsAsync())
            .OrderBy(c => c.Denomination)
            .Select(c => c.Denomination)
            .ToList()
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Pay(Dictionary<int, int> insertedCoins)
    {
        int totalInserted = insertedCoins.Sum(c => c.Key * c.Value); // Сумма добленных монет
        int totalAmount = await _cartService.GetCartTotalAsync(); // Сумма заказа
        int changeAmount = totalInserted - totalAmount; // Сумма сдачи

        if (changeAmount < 0)
        {
            ModelState.AddModelError("", "Недостаточно средств");
            return RedirectToAction(nameof(Index));
        }

        var coinsInMachine = (await _coinService.GetCoinsAsync()).ToList();
        var changeResult = CalculateChange(changeAmount, coinsInMachine);

        if (changeResult == null)
        {
            return View("Error");
        }

        var cartItems = await _cartService.GetCartItemsAsync();

        // Уменьшаем количество напитков
        foreach (var item in cartItems)
        {
            item.Drink.Quantity -= item.Quantity;
        }

        await _coinService.AddCoinsAsync(insertedCoins);

        var success = await _coinService.TryTakeChangeAsync(changeResult);
        if (!success)
        {
            return View("Error");
        }

        await _cartService.ClearCart();

        ViewBag.Change = changeResult;
        return View("Success");
    }


    private Dictionary<int, int>? CalculateChange(int changeAmount, List<Coin> coinsInMachine)
    {
        var change = new Dictionary<int, int>();

        foreach (var coin in coinsInMachine)
        {
            int need = changeAmount / coin.Denomination;
            int take = Math.Min(need, coin.Quantity);

            if (take > 0)
            {
                change[coin.Denomination] = take;
                changeAmount -= take * coin.Denomination;
            }
        }

        return changeAmount == 0 ? change : null;
    }
}
