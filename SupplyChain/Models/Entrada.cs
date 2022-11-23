using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SupplyChain.Models;

public class Entrada
{
    public int Id { get; set; }

    [DisplayName("Quantidade")]
    [Required(ErrorMessage = "A quantidade é obrigatória")]
    public int Quantidade { get; set; }

    [DisplayName("Data e Hora")]
    [Required(ErrorMessage = "A data e hora são obrigatórias")]
    public DateTime DataHora { get; set; } = DateTime.Now;

    [DisplayName("Local")]
    [Required(ErrorMessage = "O local é obrigatório")]
    public string Local { get; set; }

    [DisplayName("Mercadoria")]
    [Required(ErrorMessage = "Selecione uma mercadoria")]
    public int MercadoriaId { get; set; }
    
    public virtual Mercadoria Mercadoria { get; set; }
}
