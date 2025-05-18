using testmvcapp.Models;

namespace testmvcapp.Repositories.Interfaces;

public interface IBrandRepository
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
    Task AddBrandAsync(Brand brand);

    /// <summary>
    /// Обновить/изменить бренд
    /// </summary>
    void UpdateBrand(Brand brand);

    /// <summary>
    /// Удалить бренд
    /// </summary>
    Task DeleteBrandAsync(int id);

    /// <summary>
    /// Сохранить изменения
    /// </summary>
    Task SaveBrandChangesAsync();

    /// <summary>
    /// Проверить наличие бренда в таблице
    /// </summary>
    Task<bool> BrandExistsAsync(int id);
}