using Microsoft.AspNetCore.Mvc;

namespace RazorMVC.Controllers
{
    public class ProdutoController : Controller
    {
        /// <summary>
        /// Exibe uma lista de todos os produtos.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Exibe detalhes de um produto específico.
        /// </summary>
        /// <returns></returns>
        public IActionResult Details()
        {
            return View();
        }
        /// <summary>
        /// Permite criar um novo produto.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Permite editar um produto existente.
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            return View();
        }
        /// <summary>
        /// Permite deletar um produto existente.
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete()
        {
            return View();
        }
        /// <summary>
        /// Permite adicionar um fornecedor associado ao produto.
        /// </summary>
        /// <returns></returns>
        public IActionResult AddSupplier()
        {
            return View();
        }

    }
}
