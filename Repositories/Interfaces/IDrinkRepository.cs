using testmvcapp.Models;

namespace testmvcapp.Repositories.Interfaces;

public interface IDrinkRepository
{
    /// <summary>
    /// Получить список всех напитков (с названием бренда)
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
    /// Добавить напиток
    /// </summary>
    Task AddDrinkAsync(Drink drink);

    /// <summary>
    /// Обновить/изменить напиток
    /// </summary>
    void UpdateDrink(Drink drink);

    /// <summary>
    /// Удалить напиток
    /// </summary>
    Task DeleteDrinkAsync(int id);

    /// <summary>
    /// Сохранить изменения
    /// </summary>
    Task SaveDrinkChangesAsync();

    /// <summary>
    /// Проверить наличие напитка в таблице
    /// </summary>
    Task<bool> DrinkExistsAsync(int id);
}