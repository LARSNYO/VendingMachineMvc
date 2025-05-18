using testmvcapp.Models;
namespace testmvcapp.ViewModels;

public class PaymentViewModel
{
    /// <summary>
    /// Сумма заказа
    /// </summary>
    public int TotalAmount { get; set; }

    /// <summary>
    /// Список номиналов монет (1, 2, 5, 10)
    /// </summary>
    public List<int> Denominations { get; set; } = null!;
}
