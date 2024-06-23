using System.ComponentModel.DataAnnotations;

namespace RazorMVC.Models
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Favor adicionar um nome ao fornecedor.")]
        public string Nome { get; set; }
        [Range(900000000, 999999999, ErrorMessage = "Telefone precisa conter nove dígitos e iniciar com '9'.")]
        public int? Telefone { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}
