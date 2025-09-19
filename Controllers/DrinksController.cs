using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using testmvcapp.Services.Interfaces;
using testmvcapp.Models;
using Microsoft.EntityFrameworkCore;

namespace testmvcapp.Controllers;

[Authorize(Roles = "Admin")]
public class DrinksController : Controller
{
    private readonly IDrinkService _drinkService;
    private readonly IBrandService _brandService;

    public DrinksController(IDrinkService drinkService, IBrandService brandService)
    {
        _drinkService = drinkService;
        _brandService = brandService;
    }

    [AllowAnonymous]
    // GET: Drinks
    public async Task<IActionResult> Index()
    {
        var drinks = await _drinkService.GetDrinksAsync();
        return View(drinks);
    }

    // GET: Drinks/Create
    public async Task<IActionResult> Create()
    {
        var brands = await _brandService.GetBrandsAsync();
        ViewBag.Brands = new SelectList(brands, "Id", "Name");
        return View();
    }

    // POST: Drinks/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Drink drink)
    {
        if (ModelState.IsValid)
        {
            await _drinkService.CreateDrinkAsync(drink);
            TempData["SuccessMessage"] = "Операция успешно выполнена!";
        }

        return RedirectToAction("Index");
    }

    // GET: Drinks/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var brands = await _brandService.GetBrandsAsync();
        ViewBag.Brands = new SelectList(brands, "Id", "Name");

        var drink = await _drinkService.GetDrinkByIdAsync(id.Value);
        if (drink == null)
        {
            return NotFound();
        }
        return View(drink);
    }

    // POST: Drinks/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Quantity,ImagePath,BrandId")] Drink drink)
    {
        if (id != drink.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _drinkService.UpdateDrinkAsync(drink);
                TempData["SuccessMessage"] = "Операция успешно выполнена!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _drinkService.DrinkExistAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        var brands = await _brandService.GetBrandsAsync();
        ViewBag.Brands = new SelectList(brands, "Id", "Name");
        return View(drink);
    }


    // GET: Drinks/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var drink = await _drinkService.GetDrinkByIdAsync(id.Value);

        if (drink == null)
        {
            return NotFound();
        }

        return View(drink);
    }

    // POST: Drinks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        await _drinkService.DeleteDrinkAsync(id.Value);

        TempData["SuccessMessage"] = "Операция успешно выполнена!";
        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    // GET: Drinks/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var drink = await _drinkService.GetDrinkWithBrandByIdAsync(id.Value);
        if (drink == null)
        {
            return NotFound();
        }
        return View(drink);
    }
}