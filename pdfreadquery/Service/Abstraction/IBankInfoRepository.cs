using pdfreadquery.Models;

namespace pdfreadquery.Service.Abstraction
{
    public interface IBankInfoRepository
    {
        Task<IEnumerable<BankInfo>> SearchAsync(string searchTerm);
    }
}
