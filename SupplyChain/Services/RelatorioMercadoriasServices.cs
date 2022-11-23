using Microsoft.EntityFrameworkCore;
using SupplyChain.Data;
using SupplyChain.Models;

namespace SupplyChain.Services;

public class RelatorioMercadoriasServices
{
    private readonly AppDbContext _context;

    public RelatorioMercadoriasServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Mercadoria>> GetMercadoriaReport()
    {
        var mercadorias = await _context.Mercadorias.ToListAsync();

        if (mercadorias is null)
            return default;

        return mercadorias;
    }

    public async Task<IEnumerable<Entrada>> GetEntradaReport()
    {
        var entradas = await _context.Entradas.ToListAsync();

        if (entradas is null)
            return default;
        
        return entradas;
    }

    public async Task<IEnumerable<Saida>> GetSaidaReport()
    {
        var saidas = await _context.Saidas.ToListAsync();

        if (saidas is null)
            return default;

        return saidas;
    }
}
