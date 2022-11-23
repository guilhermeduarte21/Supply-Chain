using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupplyChain.Models;
using SupplyChain.Repositories.Interfaces;

namespace SupplyChain.Controllers
{
    public class EntradaController : Controller
    {
        private readonly IEntradaRepository _entradaRepository;
        private readonly IMercadoriaRepository _mercadoriaRepository;

        public EntradaController(IEntradaRepository entradaRepository, IMercadoriaRepository mercadoriaRepository)
        {
            _entradaRepository = entradaRepository;
            _mercadoriaRepository = mercadoriaRepository;
        }

        // GET: Entrada
        public async Task<IActionResult> Index()
        {
            var entradas = _entradaRepository.GetEntradasAsync();
            return View(await entradas);
        }

        // GET: Entrada/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var entrada = await _entradaRepository.GetEntradaAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // GET: Entrada/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.MercadoriaId = new SelectList(await _mercadoriaRepository.GetMercadoriasAsync(), "Id", "Nome");
            return View();
        }

        // POST: Entrada/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantidade,Local,MercadoriaId")] Entrada entrada)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entrada.DataHora = DateTime.Now;
                    var mercadoria = await _mercadoriaRepository.GetMercadoriaAsync(entrada.MercadoriaId);
                    mercadoria.QuantidadeEstoque += entrada.Quantidade;
                    _ = await _entradaRepository.AddEntradaAsync(entrada);
                    _ = await _mercadoriaRepository.UpdateMercadoriaAsync(mercadoria);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível salvar as alterações. Tente novamente e, se o problema persistir, entre em contato com o administrador do sistema.");
            }
            
            return View(entrada);
        }

        // GET: Entrada/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var entrada = await _entradaRepository.GetEntradaAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }
            return View(entrada);
        }

        // POST: Entrada/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantidade,DataHora,Local,MercadoriaId")] Entrada entrada)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ = await _entradaRepository.UpdateEntradaAsync(entrada);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível salvar as alterações. Tente novamente e, se o problema persistir, entre em contato com o administrador do sistema.");
            }
            
            return View(entrada);
        }

        // GET: Entrada/Delete/5
        public async Task<IActionResult> Delete(int id)
        {            
            var entrada = await _entradaRepository.GetEntradaAsync(id);

            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // POST: Entrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {               
                var entrada = await _entradaRepository.GetEntradaAsync(id);
                var mercadoria = await _mercadoriaRepository.GetMercadoriaAsync(entrada.MercadoriaId);
                mercadoria.QuantidadeEstoque -= entrada.Quantidade;
                _ = await _mercadoriaRepository.UpdateMercadoriaAsync(mercadoria);
                _ = await _entradaRepository.DeleteEntradaAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DataException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        protected override void Dispose(bool disposing)
        {
            _entradaRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
