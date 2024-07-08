using OfficeOpenXml;
using pdfreadquery.Models;
using pdfreadquery.Service.Abstraction;

namespace pdfreadquery.Service.Implementation
{
    public class ExcelService : IExcelService
    {
        private readonly PdfDbContext _context;

        public ExcelService(PdfDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BankInfo>> ReadBanksFromExcelAsync(Stream fileStream)
        {
            var bankInfos = new List<BankInfo>();

            using (var package = new ExcelPackage(fileStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming data is on the first sheet

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Start from 2 to skip header
                {
                    var bankInfo = new BankInfo
                    {
                        CountryCode = worksheet.Cells[row, 1].GetValue<string>(),
                        InstitutionName = worksheet.Cells[row, 2].GetValue<string>(),
                        PhysicalAddress1 = worksheet.Cells[row, 3].GetValue<string>(),
                        PhysicalAddress2 = worksheet.Cells[row, 4].GetValue<string>(),
                        City = worksheet.Cells[row, 5].GetValue<string>(),
                        State = worksheet.Cells[row, 6].GetValue<string>(),
                        CountryName = worksheet.Cells[row, 7].GetValue<string>(),
                        SwiftCode = worksheet.Cells[row, 8].GetValue<string>()
                    };

                    bankInfos.Add(bankInfo);
                }
            }

            return bankInfos;
        }

        public async Task SaveBankInfosAsync(IEnumerable<BankInfo> bankInfos)
        {
            _context.BankInfo.AddRange(bankInfos);
            await _context.SaveChangesAsync();
        }
    }
}
