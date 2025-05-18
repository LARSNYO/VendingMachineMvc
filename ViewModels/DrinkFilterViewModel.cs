using Microsoft.AspNetCore.Mvc.Rendering;
using testmvcapp.Models;

namespace testmvcapp.ViewModels;
public class DrinkFilterViewModel
{
    public List<Drink> Drinks { get; set; } = null!;
    public int? SelectedBrandId { get; set; }
    public string MinPrice { get; set; } = null!;
    public string MaxPrice { get; set; } = null!;
    public decimal MinAvailablePrice { get; set; }
    public decimal MaxAvailablePrice { get; set; }
    public SelectList Brands { get; set; } = null!;

    public int SelectedCount { get; set; }

    public List<int> SelectedDrinkIds { get; set; } = null!;
}