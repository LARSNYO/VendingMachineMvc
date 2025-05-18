using testmvcapp.Data;
using testmvcapp.Repositories.Interfaces;
using testmvcapp.Models;
using Microsoft.EntityFrameworkCore;

namespace testmvcapp.Repositories;

public class DrinkRepository : IDrinkRepository
{
    private readonly TestDbContext _context;
    public DrinkRepository(TestDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Drink>> GetDrinksAsync()
    {
        return await _context.Drinks.Include(d => d.Brand).ToListAsync();
    }

    public async Task<Drink?> GetDrinkByIdAsync(int id)
    {
        return await _context.Drinks.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Drink?> GetDrinkWithBrandByIdAsync(int id)
    {
        return await _context.Drinks.Include(d => d.Brand).FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task AddDrinkAsync(Drink drink)
    {
        await _context.Drinks.AddAsync(drink);
    }

    public void UpdateDrink(Drink drink)
    {
        _context.Drinks.Update(drink);
    }

    public async Task DeleteDrinkAsync(int id)
    {
        var drink = await GetDrinkByIdAsync(id);
        if (drink != null)
        {
            _context.Drinks.Remove(drink);
        }
    }

    public async Task SaveDrinkChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DrinkExistsAsync(int id)
    {
        return await _context.Drinks.AnyAsync(d => d.Id == id);
    }
}
