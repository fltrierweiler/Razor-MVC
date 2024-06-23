using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorMVC.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage= "Favor adicionar um nome ao produto.")]
        public string Nome { get; set; }
        public string? Descrição { get; set; }
        [Required(ErrorMessage = "Favor adicionar um preço ao produto.")]
        [Range(0.01, double.PositiveInfinity, ErrorMessage = "O valor do produto precisa ser maior do que 0.")]
        public decimal Preço { get; set; }
        public int? FornecedorId { get; set; }
        [Display(Name = "Data de Criação")]
        public DateTime? DataDeCriação { get; set; }

        [ForeignKey("FornecedorId")]
        public Fornecedor? Fornecedor { get; set; }
    }
}
