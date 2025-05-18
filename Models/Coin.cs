namespace testmvcapp.Models;

public class Coin
{
    /// <summary>
    /// Ключ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номинал монетки
    /// </summary>
    public int Denomination { get; set; }

    /// <summary>
    /// Количество монет в автомате
    /// </summary>
    public int Quantity { get; set; }
}
