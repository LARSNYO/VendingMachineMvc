using testmvcapp.Models;

namespace testmvcapp.Repositories.Interfaces;

public interface ICoinRepository
{
    /// <summary>
    /// Получить список всех монет
    /// </summary>
    Task<IEnumerable<Coin>> GetCoinsAsync();

    /// <summary>
    /// Получить монетку по Id
    /// </summary>
    Task<Coin?> GetCoinByIdAsync(int id);

    /// <summary>
    /// Получить номинал монеты
    /// </summary>
    Task<Coin?> GetByDenominationAsync(int denomination);

    /// <summary>
    /// Добавить монетку
    /// </summary>
    Task AddCoinAsync(Coin coin);

    /// <summary>
    /// Обновить/изменить
    /// </summary>
    void UpdateCoin(Coin coin);

    /// <summary>
    /// Удалить монетку
    /// </summary>
    Task DeleteCoinAsync(int id);

    /// <summary>
    /// Сохранить изменения 
    /// </summary>
    Task SaveCoinChangesAsync();

    /// <summary>
    /// Проверить наличие монетки в таблице
    /// </summary>
    Task<bool> CoinExistsAsync(int id);

    Task<IEnumerable<Coin>> GetAvailableCoinsAsync(); // !!! ТУТ СТРАННО ЧЕТ!!!


}