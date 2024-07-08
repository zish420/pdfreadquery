using pdfreadquery.Models;

namespace pdfreadquery.Service.Abstraction
{
    public interface IExcelProcessor
    {
        List<Product> ExtractProductsFromExcel(Stream excelStream);
        Task SendProductsToBackend(List<Product> products);
    }

}
