using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.FastReportUtils;
using SupplyChain.Services;

namespace SupplyChain.Controllers;

public class MercadoriaReportController : Controller
{
    private readonly IWebHostEnvironment _webHostEnv;
    private readonly RelatorioMercadoriasServices _relatorioMercadoriasServices;

    public MercadoriaReportController(IWebHostEnvironment webHostEnv, RelatorioMercadoriasServices relatorioMercadoriasServices)
    {
        _webHostEnv = webHostEnv;
        _relatorioMercadoriasServices = relatorioMercadoriasServices;
    }

    public async Task<IActionResult> MercadoriasReport()
    {
        var webReport = new WebReport();
        var sqLiteDataConnection = new SQLiteDataConnection();

        webReport.Report.Dictionary.AddChild(sqLiteDataConnection);

        webReport.Report.Load(Path.Combine(_webHostEnv.WebRootPath, "reports", "RelatorioMercadorias.frx"));

        var mercadorias = HelperFastReport.GetTable(await _relatorioMercadoriasServices.GetMercadoriaReport(), "MercadoriasReport");
        var entradas = HelperFastReport.GetTable(await _relatorioMercadoriasServices.GetEntradaReport(), "EntradasReport");
        var saidas = HelperFastReport.GetTable(await _relatorioMercadoriasServices.GetSaidaReport(), "SaidasReport");

        webReport.Report.RegisterData(mercadorias, "MercadoriasReport");
        webReport.Report.RegisterData(entradas, "EntradasReport");
        webReport.Report.RegisterData(saidas, "SaidasReport");

        webReport.Report.Prepare();

        Stream stream = new MemoryStream();
        webReport.Report.Export(new PDFSimpleExport(), stream);
        stream.Position = 0;

        return new FileStreamResult(stream, "application/pdf");
    }
}
