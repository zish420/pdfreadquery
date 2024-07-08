using Microsoft.EntityFrameworkCore;
using pdfreadquery.Models;
using pdfreadquery.Service.Abstraction;

namespace pdfreadquery.Service.Implementation
{
    public class BankInfoRepository : IBankInfoRepository
    {
        private readonly PdfDbContext _context;

        public BankInfoRepository(PdfDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BankInfo>> SearchAsync(string searchTerm)
        {
            // Perform a case-insensitive search on multiple fields
            return await _context.BankInfo
                .Where(b => EF.Functions.Like(b.CountryCode, $"%{searchTerm}%")
                         || EF.Functions.Like(b.InstitutionName, $"%{searchTerm}%")
                         || EF.Functions.Like(b.PhysicalAddress1, $"%{searchTerm}%")
                         || EF.Functions.Like(b.PhysicalAddress2, $"%{searchTerm}%")
                         || EF.Functions.Like(b.City, $"%{searchTerm}%")
                         || EF.Functions.Like(b.State, $"%{searchTerm}%")
                         || EF.Functions.Like(b.CountryName, $"%{searchTerm}%")
                         || EF.Functions.Like(b.SwiftCode, $"%{searchTerm}%"))
                .ToListAsync();
        }
    }
}
