using Microsoft.EntityFrameworkCore;
using testmvcapp.Data;
using testmvcapp.Repositories;
using testmvcapp.Repositories.Interfaces;
using testmvcapp.Services;
using testmvcapp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<TestDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IDrinkService, DrinkService>();
builder.Services.AddScoped<ICoinService, CoinService>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (args.Contains("seed"))
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<TestDbContext>();

            context.Database.Migrate();
            DbInitializer.DbInitialize(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при инициализации базы данных: {ex.Message}");
        }
    }
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
