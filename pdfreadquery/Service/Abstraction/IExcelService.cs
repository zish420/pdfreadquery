using pdfreadquery.Models;

namespace pdfreadquery.Service.Abstraction
{
    public interface IExcelService
    {
        Task<IEnumerable<BankInfo>> ReadBanksFromExcelAsync(Stream fileStream);
        Task SaveBankInfosAsync(IEnumerable<BankInfo> bankInfos);
    }

}
