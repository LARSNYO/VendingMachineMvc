using testmvcapp.Data;
using testmvcapp.Repositories.Interfaces;
using testmvcapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace testmvcapp.Repositories;

public class CoinRepository : ICoinRepository
{
    private readonly TestDbContext _context;
    public CoinRepository(TestDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Coin>> GetCoinsAsync()
    {
        return await _context.Coins.OrderByDescending(c => c.Denomination).ToListAsync();
    }

    public async Task<Coin?> GetCoinByIdAsync(int id)
    {
        return await _context.Coins.FindAsync(id);
    }

    public async Task<Coin?> GetByDenominationAsync(int denomination)
    {
        return await _context.Coins.FirstOrDefaultAsync(c => c.Denomination == denomination);
    }

    public async Task AddCoinAsync(Coin coin)
    {
        await _context.Coins.AddAsync(coin);
    }

    public void UpdateCoin(Coin coin)
    {
        _context.Coins.Update(coin);
    }

    public async Task DeleteCoinAsync(int id)
    {
        var coin = await GetCoinByIdAsync(id);
        if (coin != null)
        {
            _context.Coins.Remove(coin);
        }
    }

    public async Task SaveCoinChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CoinExistsAsync(int id)
    {
        return await _context.Coins.AnyAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Coin>> GetAvailableCoinsAsync() // !!!ТУТ ЧЕТО СТРАННО!!!
    {
        return await _context.Coins.Where(c => c.Quantity > 0).ToListAsync();
    }
}