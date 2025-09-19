using Microsoft.AspNetCore.Mvc;
using testmvcapp.Services.Interfaces;
using testmvcapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace testmvcapp.Controllers;

[Authorize(Roles="Admin")]
public class BrandsController : Controller
{
    private readonly IBrandService _brandService;
    public BrandsController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    // GET: Brands
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var brands = await _brandService.GetBrandsAsync();
        return View(brands);
    }

    // GET: Brands/Create
    public async Task<IActionResult> CreateAsync()
    {
        var brands = await _brandService.GetBrandsAsync();
        return View();
    }

    // POST: Brands/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(Brand brand)
    {
        if (ModelState.IsValid)
        {
            await _brandService.CreateBrandAsync(brand);
            TempData["SuccessMessage"] = "Операция успешно выполнена!";
        }

        return RedirectToAction("Index");
    }

    // GET: Brands/Edit/5
    public async Task<IActionResult> EditAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var brand = await _brandService.GetBrandByIdAsync(id.Value);
        if (brand == null)
        {
            return NotFound();
        }
        return View(brand);
    }

    // POST: Brands/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(int id, [Bind("Id, Name")] Brand brand)
    {
        if (id != brand.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _brandService.UpdateBrandAsync(brand);
                TempData["SuccessMessage"] = "Операция успешно выполнена!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _brandService.BrandExistAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        return View(brand);
    }

    // GET: Brands/Delete/5
    public async Task<IActionResult> DeleteAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var brand = await _brandService.GetBrandByIdAsync(id.Value);

        if (brand == null)
        {
            return NotFound();
        }

        return View(brand);
    }

    // POST: Drinks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        await _brandService.DeleteBrandAsync(id.Value);

        TempData["SuccessMessage"] = "Операция успешно выполнена!";
        return RedirectToAction("Index");
    }
}