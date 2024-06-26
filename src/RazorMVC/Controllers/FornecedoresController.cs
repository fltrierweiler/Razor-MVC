﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorMVC.Data;
using RazorMVC.Models;

namespace RazorMVC.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly StorageContext _context;

        public FornecedoresController(StorageContext context)
        {
            _context = context;
        }

        // GET: Fornecedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fornecedores.ToListAsync());
        }


        // GET: Fornecedores/Create
        public IActionResult Create(int? productId)
        {
            TempData["productId"] = productId;
            return View();
        }

        // POST: Fornecedores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone")] Fornecedor fornecedor)
        {
            CheckNameUniqueness(fornecedor);
            if (ModelState.IsValid)
            {
                _context.Add(fornecedor);
                await _context.SaveChangesAsync();

                if (TempData["productId"] != null)
                {
                    TempData["ToastMessage"] = $"Fornecedor '{fornecedor.Nome}' criado com sucesso. Selecione-o na lista para vinculá-lo ao produto.";
                    TempData["ToastType"] = "success";
                    if(Convert.ToInt32(TempData["productId"]) == 0)
                        return RedirectToAction("Create", "Produtos");
                    else
                        return RedirectToAction("Edit", "Produtos", new { id = TempData["productId"] });
                }
                else
                {
                    TempData["ToastMessage"] = $"Fornecedor '{fornecedor.Nome}' criado com sucesso.";
                    TempData["ToastType"] = "success";
                    return RedirectToAction(nameof(Index));
                }


            }
            return View(fornecedor);
        }
        public async Task<List<Fornecedor>> Read()
        {
            return await _context.Fornecedores.ToListAsync();
        }

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Telefone")] Fornecedor fornecedor)
        {
            if (id != fornecedor.Id)
            {
                return NotFound();
            }

            CheckNameUniqueness(fornecedor);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fornecedor);
                    await _context.SaveChangesAsync();
                    TempData["ToastMessage"] = $"Fornecedor '{fornecedor.Nome}' editado com sucesso.";
                    TempData["ToastType"] = "success";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorExists(fornecedor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor != null)
            {
                _context.Fornecedores.Remove(fornecedor);
                TempData["ToastMessage"] = $"Fornecedor '{fornecedor.Nome}' removido com sucesso.";
                TempData["ToastType"] = "success";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> LoadPartialView(int id, string view)
        {
            var produto = await _context.Fornecedores.FirstOrDefaultAsync(m => m.Id == id);
            return PartialView(view, produto);
        }

        private bool FornecedorExists(int id)
        {
            return _context.Fornecedores.Any(e => e.Id == id);
        }

        // Ao editar ou criar fornecedores, é preciso validar se já existe um fornecedor cadastrado com o mesmo nome no banco de dados (salvo pelo fornecedor sendo editado, se for o caso).
        private void CheckNameUniqueness(Fornecedor fornecedor)
        {
            if (_context.Fornecedores.Where(p => p.Id != fornecedor.Id).Any(i => i.Nome == fornecedor.Nome))
            {
                ModelState.AddModelError(nameof(fornecedor.Nome),
                                         "Já existe um fornecedor cadastrado com o mesmo nome.");
            }
        }
    }
}
