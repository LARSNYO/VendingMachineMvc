using testmvcapp.Models;

namespace testmvcapp.Repositories.Interfaces;

public interface ICartRepository
{
    /// <summary>
    /// Получить элемент корзины по cartId и drinkId
    /// </summary>
    Task<CartItem?> GetCartItemByIdAsync(string cartId, int drinkId);

    /// <summary>
    /// Получить список всех элементов корзины
    /// </summary>
    Task<IEnumerable<CartItem>> GetCartItemsAsync(string cartId);

    /// <summary>
    /// Получить общую стоимость элементов в корзине
    /// </summary>
    Task<int> GetCartTotalAsync(string cartId);

    /// <summary>
    /// Добавить элемент в корзину
    /// </summary>
    Task AddCartItemAsync(CartItem cartItem);

    /// <summary>
    /// Обновить/изменить элемент в корзине
    /// </summary>
    void UpdateCartItem(CartItem cartItem);

    /// <summary>
    /// Удалить элемент из корзины
    /// </summary>
    void RemoveCartItem(CartItem cartItem);

    /// <summary>
    /// Удалить все элементы корзины
    /// </summary>
    void ClearCart(string cartId);

    /// <summary>
    /// Сохранить изменения
    /// </summary>
    Task SaveCartItemChangesAsync();

    // Task<bool> CartItemExistsAsync(string cartId);
}