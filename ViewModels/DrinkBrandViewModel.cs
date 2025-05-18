using Microsoft.AspNetCore.Mvc.Rendering;
using testmvcapp.Models;

namespace testmvcapp.ViewModels;

public class DrinkBrandViewModel
{
    public Drink? Drink { get; set; }
    public IEnumerable<SelectListItem> Brands { get; set; } = [];
}