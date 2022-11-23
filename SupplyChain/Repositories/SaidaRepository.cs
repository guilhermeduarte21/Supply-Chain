using Microsoft.EntityFrameworkCore;
using SupplyChain.Data;
using SupplyChain.Models;
using SupplyChain.Repositories.Interfaces;

namespace SupplyChain.Repositories;

public class SaidaRepository : ISaidaRepository
{
    private readonly AppDbContext _saidarepository;

    public SaidaRepository(AppDbContext context)
    {
        _saidarepository = context;
    }
    
    public async Task<List<Saida>> GetSaidasAsync()
    {
        return _saidarepository.Saidas != null ? await _saidarepository.Saidas.Include(s => s.Mercadoria).ToListAsync() : null;
    }
    public async Task<Saida> GetSaidaAsync(int id)
    {
        if (_saidarepository.Saidas == null)
            return null;

        var saida = await _saidarepository.Saidas.Include(s => s.Mercadoria)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (saida == null)
            return null;

        return saida;
    }

    public async Task<Saida> AddSaidaAsync(Saida saida)
    {
        _saidarepository.Saidas.Add(saida);
        await _saidarepository.SaveChangesAsync();

        return saida;
    }
    public async Task<Saida> UpdateSaidaAsync(Saida saida)
    {
        _saidarepository.Entry(saida).State = EntityState.Modified;
        await _saidarepository.SaveChangesAsync();

        return saida;
    }

    public async Task<Saida> DeleteSaidaAsync(int id)
    {
        var saida = await _saidarepository.Saidas.FindAsync(id);
        if (saida == null)
            return null;

        _saidarepository.Saidas.Remove(saida);
        await _saidarepository.SaveChangesAsync();

        return saida;
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _saidarepository.Dispose();
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
