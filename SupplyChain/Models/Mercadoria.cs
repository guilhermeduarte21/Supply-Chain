using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SupplyChain.Models;

public class Mercadoria
{
    public int Id { get; set; }

    [DisplayName("Nome")]
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; }

    [DisplayName("Número de registro")]
    [Required(ErrorMessage = "O número de registro é obrigatório")]
    public string NumeroRegistro { get; set; }

    [DisplayName("Fabricante")]
    [Required(ErrorMessage = "O fabricante é obrigatório")]
    public string Fabricante { get; set; }

    [DisplayName("Tipo")]
    [Required(ErrorMessage = "O tipo é obrigatório")]
    public string Tipo { get; set; }

    [DisplayName("Descrição")]
    [Required(ErrorMessage = "A descrição é obrigatória")]
    public string Descricao { get; set; }

    [DisplayName("Estoque")]
    public int? QuantidadeEstoque { get; set; } = 0;
}
