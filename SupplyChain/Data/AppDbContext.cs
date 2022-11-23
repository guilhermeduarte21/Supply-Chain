using Microsoft.EntityFrameworkCore;
using SupplyChain.Models;

namespace SupplyChain.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{		
	}

	public DbSet<Mercadoria> Mercadorias { get; set; }
	public DbSet<Entrada> Entradas { get; set; }
	public DbSet<Saida> Saidas { get; set; }
}
