using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorMVC.Data;
using RazorMVC.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StorageContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StorageContext") ?? throw new InvalidOperationException("Connection string 'StorageContext' not found.")));


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc(options =>
{
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => $"O campo '{x}' precisa conter apenas números.");
    options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo precisa ser preenchido com um número.");
});

var app = builder.Build();

// Adicionar elementos ao banco de dados se não houver elemento algum
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedDatabase.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
