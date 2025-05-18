namespace testmvcapp.Models;

public class InsertedCoin
{
    /// <summary>
    /// Номинал монеты, внесенный покупателем
    /// </summary>
    public int Denomination { get; set; }

    /// <summary>
    /// Количество монет данного номинала, внесённых пользователем
    /// </summary>
    public int Quantity { get; set; }
}
