using Microsoft.AspNetCore.Mvc;
using pdfreadquery.Service.Abstraction;

namespace pdfreadquery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankInfoController : ControllerBase
    {
        private readonly IExcelService _excelService;

        public BankInfoController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcelAsync(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("Excel file is required.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var bankInfos = await _excelService.ReadBanksFromExcelAsync(memoryStream);

                await _excelService.SaveBankInfosAsync(bankInfos);

                return Ok(bankInfos); // Optionally, you can return a success message or the saved entities
            }
        }
    }
}
