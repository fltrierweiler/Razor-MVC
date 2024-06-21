using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorMVC.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string? Descrição { get; set; }
        [Range(0, double.PositiveInfinity, ErrorMessage = "Preço precisa ser maior do que 0.")]
        public decimal Preço { get; set; }
        public int? FornecedorId { get; set; }
        public DateTime? DataDeCriação { get; set; }

        [ForeignKey("FornecedorId")]
        public Fornecedor? Fornecedor { get; set; }
    }
}
