using SupplyChain.Models;

namespace SupplyChain.Repositories.Interfaces;

public interface IMercadoriaRepository : IDisposable
{
    Task<List<Mercadoria>> GetMercadoriasAsync();
    Task<Mercadoria> GetMercadoriaAsync(int id);
    Task<Mercadoria> AddMercadoriaAsync(Mercadoria mercadoria);
    Task<Mercadoria> UpdateMercadoriaAsync(Mercadoria mercadoria);
    Task<Mercadoria> DeleteMercadoriaAsync(int id);
}
