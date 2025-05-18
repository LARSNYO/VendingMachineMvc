namespace testmvcapp.Models;

public class CartItem
{
    /// <summary>
    /// Ключ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Количество единиц напитка в корзине
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Навигационное свойство для связанного напитка
    /// </summary>
    public Drink Drink { get; set; } = null!;

    /// <summary>
    /// Идентификатор корзины, к которой относится данный товар
    /// </summary>
    public string CartId { get; set; } = null!;
}