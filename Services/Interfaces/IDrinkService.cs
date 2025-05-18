using testmvcapp.Models;

namespace testmvcapp.Services.Interfaces;

public interface IDrinkService
{
    /// <summary>
    /// Получить список напитков
    /// </summary>
    Task<IEnumerable<Drink>> GetDrinksAsync();

    /// <summary>
    /// Получить напиток по Id
    /// </summary>
    Task<Drink?> GetDrinkByIdAsync(int id);

    /// <summary>
    /// Получить напиток с названием бренда по Id
    /// </summary>
    Task<Drink?> GetDrinkWithBrandByIdAsync(int id);

    /// <summary>
    /// Создать напиток
    /// </summary>
    Task CreateDrinkAsync(Drink drink);

    /// <summary>
    /// Обновить/изменить напиток
    /// </summary>
    Task UpdateDrinkAsync(Drink drink);

    /// <summary>
    /// Удалить напиток
    /// </summary>
    Task DeleteDrinkAsync(int id);

    /// <summary>
    /// Проверить наличие напитка в таблице
    /// </summary>
    Task<bool> DrinkExistAsync(int id);

    /// <summary>
    /// Получить отфильтрованный список напитков
    /// </summary>
    Task<IEnumerable<Drink>> GetFilterAsync(int? brandId, int? minPrice);
}