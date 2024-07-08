using Microsoft.AspNetCore.Mvc;
using pdfreadquery.Service.Abstraction;

namespace pdfreadquery.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelProcessor _excelProcessor;

        public ExcelController(IExcelProcessor excelProcessor)
        {
            _excelProcessor = excelProcessor;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;

                var products = _excelProcessor.ExtractProductsFromExcel(stream);
                await _excelProcessor.SendProductsToBackend(products);
            }

            return Ok("File processed successfully.");
        }
    }
}
