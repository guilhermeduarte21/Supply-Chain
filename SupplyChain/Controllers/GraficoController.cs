using Microsoft.AspNetCore.Mvc;
using SupplyChain.Services;

namespace SupplyChain.Controllers;

public class GraficoController : Controller
{
    private readonly GraficoEntradaSaidaMercadoriaServices _graficoEntradaSaidaMercadoriaServices;

    public GraficoController(GraficoEntradaSaidaMercadoriaServices graficoEntradaSaidaMercadoriaServices)
    {
        _graficoEntradaSaidaMercadoriaServices = graficoEntradaSaidaMercadoriaServices;
    }

    public JsonResult EntradasSaidasMercadorias(int dias)
    {
        var entradasSaidasMercadoria = _graficoEntradaSaidaMercadoriaServices.GetMercadorias(dias);
        return Json(entradasSaidasMercadoria);
    }

    [HttpGet]
    public IActionResult Index(int dias)
    {
        return View();
    }
}
