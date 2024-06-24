using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorMVC.Data;
using RazorMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona contexto de banco de dados através de injeção de dependência.
builder.Services.AddDbContext<StorageContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StorageContext") ?? throw new InvalidOperationException("Connection string 'StorageContext' not found.")));


builder.Services.AddControllersWithViews();

// Pequena tradução de validações do MessageProvider.
builder.Services.AddMvc(options =>
{
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => $"O campo '{x}' precisa conter apenas números.");
    options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo precisa ser preenchido com um número.");
});

var app = builder.Build();

// Cria migração e adiciona elementos ao banco de dados se não houver elemento algum
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StorageContext>(); 
    context.Database.Migrate(); // Cria nova migração caso não exista

    SeedDatabase.Initialize(services); // Adiciona elementos padrão ao banco
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
