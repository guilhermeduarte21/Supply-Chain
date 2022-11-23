using SupplyChain.Models;

namespace SupplyChain.Repositories.Interfaces;

public interface IEntradaRepository : IDisposable
{
    Task<List<Entrada>> GetEntradasAsync();
    Task<Entrada> GetEntradaAsync(int id);
    Task<Entrada> AddEntradaAsync(Entrada entrada);
    Task<Entrada> UpdateEntradaAsync(Entrada entrada);
    Task<Entrada> DeleteEntradaAsync(int id);
}
