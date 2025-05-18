using testmvcapp.Services.Interfaces;
using testmvcapp.Repositories.Interfaces;
using testmvcapp.Models;

namespace testmvcapp.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IEnumerable<Brand>> GetBrandsAsync()
    {
        return await _brandRepository.GetBrandsAsync();
    }

    public async Task<Brand?> GetBrandByIdAsync(int id)
    {
        return await _brandRepository.GetBrandByIdAsync(id);
    }

    public async Task CreateBrandAsync(Brand brand)
    {
        await _brandRepository.AddBrandAsync(brand);
        await _brandRepository.SaveBrandChangesAsync();
    }

    public async Task UpdateBrandAsync(Brand brand)
    {
        _brandRepository.UpdateBrand(brand);
        await _brandRepository.SaveBrandChangesAsync();
    }

    public async Task DeleteBrandAsync(int id)
    {
        await _brandRepository.DeleteBrandAsync(id);
        await _brandRepository.SaveBrandChangesAsync();
    }

    public async Task<bool> BrandExistAsync(int id)
    {
        return await _brandRepository.BrandExistsAsync(id);
    }
}