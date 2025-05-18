namespace testmvcapp.Models;

public class Brand
{
    /// <summary>
    /// Ключ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название бренда, пример: "Coca-cola"
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Коллекция напитков, связанных с этим брендом
    /// </summary>
    public ICollection<Drink>? Drinks { get; set; }
}