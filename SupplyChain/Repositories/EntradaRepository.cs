using Microsoft.EntityFrameworkCore;
using SupplyChain.Data;
using SupplyChain.Models;
using SupplyChain.Repositories.Interfaces;

namespace SupplyChain.Repositories;

public class EntradaRepository : IEntradaRepository
{
    private readonly AppDbContext _entradarepository;

    public EntradaRepository(AppDbContext context)
    {
        _entradarepository = context;
    }

    public async Task<List<Entrada>> GetEntradasAsync()
    {
        return _entradarepository.Entradas != null ? await _entradarepository.Entradas.Include(e => e.Mercadoria).ToListAsync() : null;
    }

    public async Task<Entrada> GetEntradaAsync(int id)
    {
        if (_entradarepository.Entradas == null)
            return null;

        var entrada = await _entradarepository.Entradas.Include(e => e.Mercadoria)
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (entrada == null)
            return null;

        return entrada;
    }

    public async Task<Entrada> AddEntradaAsync(Entrada entrada)
    {
        _entradarepository.Entradas.Add(entrada);
        await _entradarepository.SaveChangesAsync();

        return entrada;
    }
    
    public async Task<Entrada> UpdateEntradaAsync(Entrada entrada)
    {
        _entradarepository.Entry(entrada).State = EntityState.Modified;
        await _entradarepository.SaveChangesAsync();

        return entrada;
    }

    public async Task<Entrada> DeleteEntradaAsync(int id)
    {
        var entrada = await _entradarepository.Entradas.FindAsync(id);
        if (entrada == null)
            return null;

        _entradarepository.Entradas.Remove(entrada);
        await _entradarepository.SaveChangesAsync();

        return entrada;
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _entradarepository.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
