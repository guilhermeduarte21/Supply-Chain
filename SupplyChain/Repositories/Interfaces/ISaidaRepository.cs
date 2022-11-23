using SupplyChain.Models;

namespace SupplyChain.Repositories.Interfaces;

public interface ISaidaRepository : IDisposable
{
    Task<List<Saida>> GetSaidasAsync();
    Task<Saida> GetSaidaAsync(int id);
    Task<Saida> AddSaidaAsync(Saida saida);
    Task<Saida> UpdateSaidaAsync(Saida saida);
    Task<Saida> DeleteSaidaAsync(int id);
}
