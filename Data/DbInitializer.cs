using testmvcapp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using testmvcapp.Data;

namespace testmvcapp.Data;
/// <summary>
/// Здесь создается класс для инициализации БД тестовыми данными
/// </summary>
public static class DbInitializer
{
    public static void DbInitialize(TestDbContext context)
    {
        context.Drinks.RemoveRange(context.Drinks);
        context.Brands.RemoveRange(context.Brands);
        context.Coins.RemoveRange(context.Coins);
        context.SaveChanges();
        
        // Проверяем таблицу, чтобы не сидить повторно
        if (context.Brands.Any())
        {
            return; // Если сиды есть - ничего не делаем 
        }


        var brands = new Brand[]
        {
            new Brand { Name = "Coca-Cola" },
            new Brand { Name = "Dr. Pepper" },
            new Brand { Name = "Fanta" },
            new Brand { Name = "Sprite" },
        };
        context.Brands.AddRange(brands);
        context.SaveChanges();

        var drinks = new Drink[]
        {
            new Drink { Name = "Coca-Cola Classic", Price = 100, Quantity = 10, ImagePath = "/img/Drinks/coca-cola_classic.jpg", BrandId = brands[0].Id },
            new Drink { Name = "Coca-Cola Zero", Price = 120, Quantity = 7, ImagePath = "img/Drinks/coca-cola_zero.jpg", BrandId = brands[0].Id },
            new Drink { Name = "Dr. Pepper Original", Price = 200, Quantity = 3, ImagePath = "img/Drinks/dr_pepper_original.jpg", BrandId = brands[1].Id },
            new Drink { Name = "Dr. Pepper Zero", Price = 250, Quantity = 5, ImagePath = "/img/Drinks/dr_pepper_zero.jpg", BrandId = brands[1].Id },
            new Drink { Name = "Fanta Orange", Price = 50, Quantity = 15, ImagePath = "/img/Drinks/fanta_orange.jpg", BrandId = brands[2].Id },
            new Drink { Name = "Fanta Grape", Price = 150, Quantity = 13, ImagePath = "/img/Drinks/fanta_grape.jpg", BrandId = brands[2].Id },
            new Drink { Name = "Fanta Lemon", Price = 90, Quantity = 10, ImagePath = "/img/Drinks/fanta_lemon.jpg", BrandId = brands[2].Id },
            new Drink { Name = "Sprite", Price = 30, Quantity = 70, ImagePath = "img/Drinks/sprite.jpg", BrandId = brands[3].Id },
            new Drink { Name = "Sprite Cranberry", Price = 350, Quantity = 0, ImagePath = "/img/Drinks/sprite_cranberry.jpg", BrandId = brands[3].Id },
        };
        context.Drinks.AddRange(drinks);
        context.SaveChanges();

        var coins = new Coin[]
        {
            new Coin { Denomination = 1, Quantity = 0 },
            new Coin { Denomination = 2, Quantity = 0 },
            new Coin { Denomination = 5, Quantity = 0 },
            new Coin { Denomination = 10, Quantity = 0 },
        };
        context.Coins.AddRange(coins);
        context.SaveChanges();
    }
}