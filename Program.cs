using Microsoft.EntityFrameworkCore;
using testmvcapp.Data;
using testmvcapp.Models;
using testmvcapp.Repositories;
using testmvcapp.Repositories.Interfaces;
using testmvcapp.Services;
using testmvcapp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<TestDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();


// DI контейнеры
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
    // options.IdleTimeout = TimeSpan.FromSeconds(10);
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// // Вызов инициализации Db
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     try
//     {
//         var context = services.GetRequiredService<TestDbContext>();

//         // context.Database.Migrate(); // Применение миграций 
//         DbInitializer.DbInitialize(context); // Заполнение тестовыми данными
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"Ошибка при инициализации базы данных: {ex.Message}");
//     }
// }

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
