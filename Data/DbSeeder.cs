using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using testmvcapp.Models;

namespace testmvcapp.Data;

public static class DbSeeder
{
    public static async Task DbInitialize(TestDbContext context)
    {
        context.Drinks.RemoveRange(context.Drinks);
        context.Brands.RemoveRange(context.Brands);
        context.Coins.RemoveRange(context.Coins);

        await context.SaveChangesAsync();

        if (await context.Brands.AnyAsync())
        {
            return;
        }

        var brands = new Brand[]
        {
            new Brand { Name = "Coca-Cola" },
            new Brand { Name = "Dr. Pepper" },
            new Brand { Name = "Fanta" },
            new Brand { Name = "Sprite" },
        };
        await context.Brands.AddRangeAsync(brands);
        await context.SaveChangesAsync();

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

        await context.Drinks.AddRangeAsync(drinks);
        await context.SaveChangesAsync();

        var coins = new Coin[]
        {
            new Coin { Denomination = 1, Quantity = 5 },
            new Coin { Denomination = 2, Quantity = 5 },
            new Coin { Denomination = 5, Quantity = 5 },
            new Coin { Denomination = 10, Quantity = 5 },
        };

        await context.Coins.AddRangeAsync(coins);
        await context.SaveChangesAsync();
    }

    public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string adminEmail = "admin@example.com";
        string adminPassword = "Admin123!";

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                Console.WriteLine("Failed to create admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}