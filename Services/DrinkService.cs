using testmvcapp.Services.Interfaces;
using testmvcapp.Repositories.Interfaces;
using testmvcapp.Models;
using testmvcapp.Repositories;

namespace testmvcapp.Services;

public class DrinkService : IDrinkService
{
    private readonly IDrinkRepository _drinkRepository;
    public DrinkService(IDrinkRepository drinkRepository)
    {
        _drinkRepository = drinkRepository;
    }
    public async Task<IEnumerable<Drink>> GetDrinksAsync()
    {
        return await _drinkRepository.GetDrinksAsync();
    }

    public async Task<Drink?> GetDrinkByIdAsync(int id)
    {
        return await _drinkRepository.GetDrinkByIdAsync(id);
    }

    public async Task<Drink?> GetDrinkWithBrandByIdAsync(int id)
    {
        return await _drinkRepository.GetDrinkWithBrandByIdAsync(id);
    }

    public async Task CreateDrinkAsync(Drink drink)
    {
        await _drinkRepository.AddDrinkAsync(drink);
        await _drinkRepository.SaveDrinkChangesAsync();
    }

    public async Task UpdateDrinkAsync(Drink drink)
    {
        _drinkRepository.UpdateDrink(drink);
        await _drinkRepository.SaveDrinkChangesAsync();
    }

    public async Task DeleteDrinkAsync(int id)
    {
        await _drinkRepository.DeleteDrinkAsync(id);
        await _drinkRepository.SaveDrinkChangesAsync();
    }

    public async Task<bool> DrinkExistAsync(int id)
    {
        return await _drinkRepository.DrinkExistsAsync(id);
    }

    public async Task<IEnumerable<Drink>> GetFilterAsync(int? brandId, int? minPrice)
    {
        var drinks = await _drinkRepository.GetDrinksAsync();
        var filtered = drinks.AsQueryable();

        if (brandId.HasValue)
        {
            filtered = filtered.Where(d => d.BrandId == brandId);
        }

        if (minPrice.HasValue)
        {
            filtered = filtered.Where(d => d.Price >= minPrice);
        }
        
        return filtered.ToList();
    }
}