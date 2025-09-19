using Microsoft.AspNetCore.Mvc;
using testmvcapp.Services.Interfaces;
using testmvcapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace testmvcapp.Controllers;

[Authorize(Roles = "Admin")]
public class CoinsController : Controller
{
    private readonly ICoinService _coinService;
    public CoinsController(ICoinService coinService)
    {
        _coinService = coinService;
    }

    // GET: Coins
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var coins = await _coinService.GetCoinsAsync();
        return View(coins);
    }

    // GET: Coins/Create
    public async Task<IActionResult> CreateAsync()
    {
        var coins = await _coinService.GetCoinsAsync();
        return View();
    }

    // POST: Coins/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(Coin coin)
    {
        if (ModelState.IsValid)
        {
            await _coinService.CreateCoinAsync(coin);
            TempData["SuccessMessage"] = "Операция успешно выполнена!";
        }

        return RedirectToAction("Index");
    }

    // GET: Coins/Edit/5
    public async Task<IActionResult> EditAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var coin = await _coinService.GetCoinByIdAsync(id.Value);
        if (coin == null)
        {
            return NotFound();
        }
        return View(coin);
    }

    // POST: Coins/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(int id, [Bind("Id, Denomination, Quantity ")] Coin coin)
    {
        if (id != coin.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _coinService.UpdateCoinAsync(coin);
                TempData["SuccessMessage"] = "Операция успешно выполнена!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _coinService.CoinExistAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        return View(coin);
    }

    // GET: Coins/Delete/5
    public async Task<IActionResult> DeleteAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var coin = await _coinService.GetCoinByIdAsync(id.Value);

        if (coin == null)
        {
            return NotFound();
        }

        return View(coin);
    }

    // POST: Coins/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        await _coinService.DeleteCoinAsync(id.Value);

        TempData["SuccessMessage"] = "Операция успешно выполнена!";
        return RedirectToAction("Index");
    }
}