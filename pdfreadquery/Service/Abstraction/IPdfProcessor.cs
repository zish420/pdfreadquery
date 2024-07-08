using pdfreadquery.Models;

namespace pdfreadquery.Service.Abstraction
{
    public interface IPdfProcessor
    {
        List<Product> ExtractProducts(Stream pdfStream);
        Task SendProductsToBackend(List<Product> products);
    }
}
