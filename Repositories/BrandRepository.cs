using testmvcapp.Data;
using testmvcapp.Repositories.Interfaces;
using testmvcapp.Models;
using Microsoft.EntityFrameworkCore;

namespace testmvcapp.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly TestDbContext _context;

    public BrandRepository(TestDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Brand>> GetBrandsAsync()
    {
        return await _context.Brands.ToListAsync();
    }

    public async Task<Brand?> GetBrandByIdAsync(int id)
    {
        return await _context.Brands.FindAsync(id);
    }

    public async Task AddBrandAsync(Brand brand)
    {
        await _context.Brands.AddAsync(brand);
    }

    public void UpdateBrand(Brand brand)
    {
        _context.Brands.Update(brand);
    }

    public async Task DeleteBrandAsync(int id)
    {
        var brand = await GetBrandByIdAsync(id);
        if (brand != null)
        {
            _context.Brands.Remove(brand);
        }
    }

    public async Task SaveBrandChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> BrandExistsAsync(int id)
    {
        return await _context.Brands.AnyAsync(b => b.Id == id);
    }
}