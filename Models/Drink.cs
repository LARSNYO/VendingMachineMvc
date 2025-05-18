namespace testmvcapp.Models;

public class Drink
{
    /// <summary>
    /// Ключ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название напитка, пример: "Coca-cola Zero"
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Цена
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// Количество товара в автомате
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Путь к изображению
    /// </summary>
    public string? ImagePath { get; set; }

    /// <summary>
    /// Идентификатор бренда (внешний ключ) 
    /// </summary>
    public int BrandId { get; set; }

    /// <summary>
    /// Навигационное свойство для связанного бренда
    /// </summary>
    public Brand? Brand { get; set; }
}