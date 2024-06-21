using Microsoft.AspNetCore.Mvc;

namespace RazorMVC.Controllers
{
    public class FornecedorController : Controller
    {
        /// <summary>
        /// Permite criar um novo fornecedor.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Permite ler a lista de fornecedores.
        /// </summary>
        /// <returns></returns>
        public IActionResult Read()
        {
            return View();
        }
        /// <summary>
        /// Permite editar um fornecedor existente.
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            return View();
        }
        /// <summary>
        /// Permite deletar um fornecedor existente.
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete()
        {
            return View();
        }
    }
}
