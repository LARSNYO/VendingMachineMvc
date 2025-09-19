using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using testmvcapp.Models;

namespace testmvcapp.Data;

public class TestDbContext : IdentityDbContext<IdentityUser>
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    { }
    public DbSet<Drink> Drinks { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Coin> Coins { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
}


