using Microsoft.EntityFrameworkCore;
using SupplyChain.Data;
using SupplyChain.Models;
using SupplyChain.Repositories.Interfaces;

namespace SupplyChain.Repositories;

public class MercadoriaRepository : IMercadoriaRepository
{
  private readonly AppDbContext _mercadoriarepository;

  public MercadoriaRepository(AppDbContext context)
  {
    _mercadoriarepository = context;
  }

  public async Task<List<Mercadoria>> GetMercadoriasAsync()
  {
    return _mercadoriarepository.Mercadorias != null ? await _mercadoriarepository.Mercadorias.ToListAsync() : null;
  }

  public async Task<Mercadoria> GetMercadoriaAsync(int id)
  {
    if (_mercadoriarepository.Mercadorias == null)
      return null;

    var mercadoria = await _mercadoriarepository.Mercadorias
        .FirstOrDefaultAsync(m => m.Id == id);

    if (mercadoria == null)
      return null;

    return mercadoria;
  }

  public async Task<Mercadoria> AddMercadoriaAsync(Mercadoria mercadoria)
  {
    _mercadoriarepository.Mercadorias.Add(mercadoria);
    await _mercadoriarepository.SaveChangesAsync();

    return mercadoria;
  }

  public async Task<Mercadoria> UpdateMercadoriaAsync(Mercadoria mercadoria)
  {
    _mercadoriarepository.Entry(mercadoria).State = EntityState.Modified;
    await _mercadoriarepository.SaveChangesAsync();

    return mercadoria;
  }

  public async Task<Mercadoria> DeleteMercadoriaAsync(int id)
  {
    var mercadoria = await _mercadoriarepository.Mercadorias.FindAsync(id);
    if (mercadoria == null)
      return null;

    _mercadoriarepository.Mercadorias.Remove(mercadoria);
    await _mercadoriarepository.SaveChangesAsync();

    return mercadoria;
  }

  private bool disposed = false;

  protected virtual void Dispose(bool disposing)
  {
    if (!this.disposed)
    {
      if (disposing)
      {
        _mercadoriarepository.Dispose();
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
