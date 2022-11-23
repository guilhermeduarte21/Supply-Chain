using SupplyChain.Data;
using SupplyChain.Models;

namespace SupplyChain.Services;

public class GraficoEntradaSaidaMercadoriaServices
{
    private readonly AppDbContext _context;

    public GraficoEntradaSaidaMercadoriaServices(AppDbContext context)
    {
        _context = context;
    }

    public List<EntradaSaidaMercadoriaGrafico> GetMercadorias(int dias = 360)
    {
        var mercadorias = _context.Mercadorias.ToList();
        var entradaSaidaMercadoriaGrafico = new List<EntradaSaidaMercadoriaGrafico>();
        
        foreach (var mercadoria in mercadorias)
        {
            var _entradaSaidaMercadoriaGrafico = new EntradaSaidaMercadoriaGrafico
            {
                Mercadoria = mercadoria.Nome,
                Entradas = _context.Entradas.Where(e => e.MercadoriaId == mercadoria.Id && e.DataHora >= DateTime.Now.AddDays(-dias)).Sum(q => q.Quantidade),
                Saidas = _context.Saidas.Where(s => s.MercadoriaId == mercadoria.Id && s.DataHora >= DateTime.Now.AddDays(-dias)).Sum(q => q.Quantidade)
            };
            
            entradaSaidaMercadoriaGrafico.Add(_entradaSaidaMercadoriaGrafico);
        }

        return entradaSaidaMercadoriaGrafico;
    }
}
