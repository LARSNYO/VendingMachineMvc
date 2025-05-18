using testmvcapp.Models;
namespace testmvcapp.Services.Interfaces;
public interface ICoinService
{
    Task<IEnumerable<Coin>> GetCoinsAsync();
    Task<Coin?> GetByDenominationAsync(int denomination);
    Task AddCoinsAsync(Dictionary<int, int> insertedCoins);

    /// <summary>
    /// Получить монетку по Id
    /// </summary>
    Task<Coin?> GetCoinByIdAsync(int id);

    /// <summary>
    /// Создать бренд
    /// </summary>
    Task CreateCoinAsync(Coin coin);

    /// <summary>
    /// Обновить/изменить бренд
    /// </summary>
    Task UpdateCoinAsync(Coin coin);

    /// <summary>
    /// Удалить монетку
    /// </summary>
    Task DeleteCoinAsync(int id);

    /// <summary>
    /// Проверить наличие монетки в таблице
    /// </summary>
    Task<bool> CoinExistAsync(int id);
    Task<bool> TryTakeChangeAsync(Dictionary<int, int> changeCoins);
}
