using testmvcapp.Models;

namespace testmvcapp.Services.Interfaces;

public interface ICartService
{
    /// <summary>
    /// Получить CartId
    /// </summary>
    string GetCartId();

    /// <summary>
    /// Получить список всех элементов корзины
    /// </summary>
    Task<IEnumerable<CartItem>> GetCartItemsAsync();

    /// <summary>
    /// Получить общую стоимость элементов в корзине
    /// </summary>
    Task<int> GetCartTotalAsync();

    /// <summary>
    /// Добавить элемент в корзину
    /// </summary>
    Task AddToCartAsync(Drink drink, int quantity);

    /// <summary>
    /// Удалить элемент из корзины
    /// </summary>
    Task RemoveFromCartAsync(Drink drink);

    /// <summary>
    /// Увеличить количество товара в корзине на 1ед.
    /// </summary>
    Task<int> IncreaseQuantityAsync(Drink drink);

    /// <summary>
    /// Уменьшить количество товара в корзине на 1ед.
    /// </summary>
    Task<int> ReduceQuantityAsync(Drink drink);

    /// <summary>
    /// Очистить корзину
    /// </summary>
    Task ClearCart();
}