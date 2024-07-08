using Microsoft.AspNetCore.Mvc;
using pdfreadquery.Models;
using pdfreadquery.Service.Abstraction;

namespace pdfreadquery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IBankInfoRepository _bankInfoRepository;

        public SearchController(IBankInfoRepository bankInfoRepository)
        {
            _bankInfoRepository = bankInfoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankInfo>>> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var results = await _bankInfoRepository.SearchAsync(searchTerm);

            return Ok(results);
        }
    }
}
