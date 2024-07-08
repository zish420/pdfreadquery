using Microsoft.AspNetCore.Mvc;
using pdfreadquery.Service.Abstraction;

namespace pdfreadquery.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IPdfProcessor _pdfProcessor;

        public PdfController(IPdfProcessor pdfProcessor)
        {
            _pdfProcessor = pdfProcessor;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdf(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;

                var products = _pdfProcessor.ExtractProducts(stream);
                await _pdfProcessor.SendProductsToBackend(products);
            }

            return Ok("File processed successfully.");
        }
    }
}
