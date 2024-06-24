using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorMVC.Data;
using RazorMVC.Models;

namespace RazorMVC.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly StorageContext _context;

        public ProdutosController(StorageContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            List<Produto> produtos = await _context.Produtos.Include(p => p.Fornecedor).ToListAsync();
            return View(produtos);
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.Include(p=> p.Fornecedor).FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public async Task<IActionResult> Create()
        {
            await SetSuppliersViewBag();
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descrição,Preço,FornecedorId,DataDeCriação")] Produto produto)
        {
            CheckNameUniqueness(produto);
            if (ModelState.IsValid)
            {
                var price = produto.Preço;
                produto.FornecedorId = produto.FornecedorId == 0 ? null : produto.FornecedorId; // Caso o usuário escolha "Nenhum" como fornecedor.
                _context.Add(produto);
                await _context.SaveChangesAsync();
                TempData["ToastMessage"] = $"'{produto.Nome}' criado com sucesso!";
                TempData["ToastType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            await SetSuppliersViewBag();

            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            await SetSuppliersViewBag();
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descrição,Preço,FornecedorId,DataDeCriação")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            CheckNameUniqueness(produto);

            if (ModelState.IsValid)
            {
                try
                {
                    produto.FornecedorId = produto.FornecedorId == 0 ? null : produto.FornecedorId; // Caso o usuário escolha "Nenhum" como fornecedor.
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["ToastMessage"] = $"'{produto.Nome}' editado com sucesso!";
                TempData["ToastType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            await SetSuppliersViewBag();
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                TempData["ToastMessage"] = $"'{produto.Nome}' removido com sucesso!";
                TempData["ToastType"] = "success";
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddSupplier(int id)
        {
            return RedirectToAction("Create", "Fornecedores", new { productId = id });
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
        // Tarefa responsável por listar os fornecedores existentes ao criar ou editar produtos.
        private async Task SetSuppliersViewBag()
        {
            List<Fornecedor> fornecedores = await _context.Fornecedores.ToListAsync();
            ViewBag.Fornecedores = new SelectList(fornecedores, "Id", "Nome");
        }
        // Ao editar ou criar produtos, é preciso validar se já existe um produto cadastrado com o mesmo nome no banco de dados (salvo pelo produto sendo editado, se for o caso).
        private void CheckNameUniqueness(Produto produto)
        {
            if (_context.Produtos.Where(p => p.Id != produto.Id).Any(i => i.Nome == produto.Nome))
            {
                ModelState.AddModelError(nameof(produto.Nome),
                                         "Já existe um produto cadastrado com o mesmo nome.");
            }
        }
    }
}
