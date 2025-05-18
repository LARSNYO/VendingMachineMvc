using testmvcapp.Models;

namespace testmvcapp.Services.Interfaces;

public interface IBrandService
{
    /// <summary>
    /// Получить список брендов
    /// </summary>
    Task<IEnumerable<Brand>> GetBrandsAsync();

    /// <summary>
    /// Получить бренд по Id
    /// </summary>
    Task<Brand?> GetBrandByIdAsync(int id);

    /// <summary>
    /// Создать бренд
    /// </summary>
    Task CreateBrandAsync(Brand brand);

    /// <summary>
    /// Обновить/изменить бренд
    /// </summary>
    Task UpdateBrandAsync(Brand brand);

    /// <summary>
    /// Удалить бренд
    /// </summary>
    Task DeleteBrandAsync(int id);

    /// <summary>
    /// Проверить наличие бренда в таблице
    /// </summary>
    Task<bool> BrandExistAsync(int id);
}