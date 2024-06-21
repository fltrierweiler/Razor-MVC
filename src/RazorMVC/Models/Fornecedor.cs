using System.ComponentModel.DataAnnotations;

namespace RazorMVC.Models
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public int? Telefone { get; set; }
        public ICollection<Produto> Produtos { get; set; }

    }
}
