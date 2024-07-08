using pdfreadquery.Models;
using pdfreadquery.Service.Abstraction;
using System.Text;
using System.Text.Json;
using UglyToad.PdfPig;

namespace pdfreadquery.Service.Implementation
{
    public class PdfProcessor : IPdfProcessor
    {
        public List<Product> ExtractProducts(Stream pdfStream)
        {
            List<Product> products = new List<Product>();

            using (var pdfDocument = PdfDocument.Open(pdfStream))
            {
                foreach (var page in pdfDocument.GetPages())
                {
                    var text = page.Text;
                    var productEntries = text.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var entry in productEntries)
                    {
                        var product = ParseProduct(entry);
                        if (product != null)
                        {
                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }

        public async Task SendProductsToBackend(List<Product> products)
        {
            var client = new HttpClient();
            var json = JsonSerializer.Serialize(products);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://your-backend-url/api/endpoint", content);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error
                throw new Exception("Failed to send data to the backend.");
            }
        }

        private Product ParseProduct(string entry)
        {
            var lines = entry.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 5) return null;

            try
            {
                var product = new Product
                {
                    Id = int.Parse(lines[0]),
                    Name = lines[1],
                    Description = lines[2],
                    Price = decimal.Parse(lines[3]),
                    ImageUrl = lines[4]
                };
                return product;
            }
            catch
            {
                // Handle parsing errors
                return null;
            }
        }
    }
}
