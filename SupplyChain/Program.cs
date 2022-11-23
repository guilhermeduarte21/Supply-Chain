using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Data;
using SupplyChain.Repositories;
using SupplyChain.Repositories.Interfaces;
using SupplyChain.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

FastReport.Utils.RegisteredObjects.AddConnection(typeof(FastReport.Data.SQLiteDataConnection));

builder.Services.AddTransient<IMercadoriaRepository, MercadoriaRepository>();
builder.Services.AddTransient<IEntradaRepository, EntradaRepository>();
builder.Services.AddTransient<ISaidaRepository, SaidaRepository>();

builder.Services.AddScoped<GraficoEntradaSaidaMercadoriaServices>();
builder.Services.AddScoped<RelatorioMercadoriasServices>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseFastReport();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Mercadoria}/{action=Index}/{id?}");

app.Run();
