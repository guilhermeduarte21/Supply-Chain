using Microsoft.AspNetCore.Mvc;
using SupplyChain.Models;
using SupplyChain.Repositories.Interfaces;
using System.Data;

namespace SupplyChain.Controllers;

public class MercadoriaController : Controller
{
  private readonly IMercadoriaRepository _mercadoriaRepository;

  public MercadoriaController(IMercadoriaRepository mercadoriaRepository)
  {
    _mercadoriaRepository = mercadoriaRepository;
  }

  // GET: Mercadoria
  public async Task<IActionResult> Index()
  {
    var mercadorias = _mercadoriaRepository.GetMercadoriasAsync();
    return View(await mercadorias);
  }

  // GET: Mercadoria/Details/5
  public async Task<IActionResult> Details(int id)
  {
    var mercadoria = await _mercadoriaRepository.GetMercadoriaAsync(id);
    if (mercadoria == null)
    {
        return NotFound();
    }

    return View(mercadoria);
  }

  // GET: Mercadoria/Create
  public IActionResult Create()
  {
    return View();
  }

  // POST: Mercadoria/Create
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create([Bind("Nome, NumeroRegistro, Fabricante, Tipo, Descricao")] Mercadoria mercadoria)
  {
        try
        {
            if (ModelState.IsValid)
            {
                _ = await _mercadoriaRepository.AddMercadoriaAsync(mercadoria);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (DataException)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível salvar as alterações. Tente novamente e, se o problema persistir, entre em contato com o administrador do sistema.");
        }
        return View(mercadoria);
    }

  // GET: Mercadoria/Edit/5
  public async Task<IActionResult> Edit(int id)
  {
    var mercadoria = await _mercadoriaRepository.GetMercadoriaAsync(id);
    if (mercadoria == null)
    {
      return NotFound();
    }
    return View(mercadoria);
  }

  // POST: Mercadoria/Edit/5
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, [Bind("Id, Nome, NumeroRegistro, Fabricante, Tipo, Descricao, QuantidadeEstoque")] Mercadoria mercadoria)
  {
    try
    {
        if (ModelState.IsValid)
        {
            _ = await _mercadoriaRepository.UpdateMercadoriaAsync(mercadoria);
            return RedirectToAction(nameof(Index));
        }
    }
    catch (DataException)
    {
        ModelState.AddModelError(string.Empty, "Não foi possível salvar as alterações. Tente novamente e, se o problema persistir, entre em contato com o administrador do sistema.");
    }
    return View(mercadoria);
  }

  // GET: Mercadoria/Delete/5
  public async Task<IActionResult> Delete(bool? saveChangesError = false, int id = 0)
  {
    if (saveChangesError.GetValueOrDefault())
    {
        ViewBag.ErrorMessage = "Falha ao excluir. Tente novamente e, se o problema persistir, consulte o administrador do sistema.";
    }

    Mercadoria mercadoria = await _mercadoriaRepository.GetMercadoriaAsync(id);
    if (mercadoria == null)
    {
      return NotFound();
    }

    return View(mercadoria);
  }

  // POST: Mercadoria/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
        try
        {
            _ = await _mercadoriaRepository.DeleteMercadoriaAsync(id);
        }
        catch (DataException)
        {
            return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
        }
        return RedirectToAction(nameof(Index));
  }

  protected override void Dispose(bool disposing)
  {
    _mercadoriaRepository.Dispose();
    base.Dispose(disposing);
  }
}
