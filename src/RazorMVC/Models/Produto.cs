namespace RazorMVC.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public decimal Preço { get; set; }
        public int FornecedorID { get; set; }
        public DateTime DataDeCriação { get; set; }
    }
}
