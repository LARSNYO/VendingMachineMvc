using testmvcapp.Models;

namespace testmvcapp.ViewModels;

public class CartViewModel
{
    public List<CartItem> CartItems { get; set; } = new();
    public int Total { get; set; }  
}