using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Data;
using SupplyChain.Models;
using SupplyChain.Repositories;
using SupplyChain.Repositories.Interfaces;

namespace SupplyChain.Controllers
{
    public class SaidaController : Controller
    {
        private readonly ISaidaRepository _saidaRepository;
        private readonly IMercadoriaRepository _mercadoriaRepository;

        public SaidaController(ISaidaRepository saidaRepository, IMercadoriaRepository mercadoriaRepository)
        {
            _saidaRepository = saidaRepository;
            _mercadoriaRepository = mercadoriaRepository;
        }

        // GET: Saida
        public async Task<IActionResult> Index()
        {
            var saidas = _saidaRepository.GetSaidasAsync();
            return View(await saidas);
        }

        // GET: Saida/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var saida = await _saidaRepository.GetSaidaAsync(id);
            if (saida == null)
            {
                return NotFound();
            }

            return View(saida);
        }

        // GET: Saida/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.MercadoriaId = new SelectList(await _mercadoriaRepository.GetMercadoriasAsync(), "Id", "Nome");
            return View();
        }

        // POST: Saida/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantidade,Local,MercadoriaId")] Saida saida)
        {
            try
            {
                if (ModelState.IsValid)
                {                   
                    var mercadoria = await _mercadoriaRepository.GetMercadoriaAsync(saida.MercadoriaId);
                    
                    if (mercadoria.QuantidadeEstoque >= saida.Quantidade)
                    {
                        saida.DataHora = DateTime.Now;
                        mercadoria.QuantidadeEstoque -= saida.Quantidade;
                        _ = await _saidaRepository.AddSaidaAsync(saida);
                        _ = await _mercadoriaRepository.UpdateMercadoriaAsync(mercadoria);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.MercadoriaId = new SelectList(_mercadoriaRepository.GetMercadoriasAsync().Result, "Id", "Nome");
                        ModelState.AddModelError("Quantidade", "Quantidade de saída maior que a quantidade em estoque");
                    }
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(saida);
        }

        // GET: Saida/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var saida = await _saidaRepository.GetSaidaAsync(id);
            if (saida == null)
            {
                return NotFound();
            }
            return View(saida);
        }

        // POST: Saida/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantidade,DataHora,Local,MercadoriaId")] Saida saida)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ = await _saidaRepository.UpdateSaidaAsync(saida);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível salvar as alterações. Tente novamente e, se o problema persistir, entre em contato com o administrador do sistema.");
            }

            return View(saida);
        }

        // GET: Saida/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var saida = await _saidaRepository.GetSaidaAsync(id);
            if (saida == null)
            {
                return NotFound();
            }

            return View(saida);
        }

        // POST: Saida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var saida = await _saidaRepository.GetSaidaAsync(id);
                var mercadoria = await _mercadoriaRepository.GetMercadoriaAsync(saida.MercadoriaId);
                mercadoria.QuantidadeEstoque += saida.Quantidade;
                _ = await _mercadoriaRepository.UpdateMercadoriaAsync(mercadoria);
                _ = await _saidaRepository.DeleteSaidaAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DataException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            _saidaRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
