using OfficeOpenXml;
using pdfreadquery.Models;
using pdfreadquery.Service.Abstraction;
using System.Text;
using System.Text.Json;

namespace pdfreadquery.Service.Implementation
{
    public class ExcelProcessor : IExcelProcessor
    {
        public List<Product> ExtractProductsFromExcel(Stream excelStream)
        {
            List<Product> products = new List<Product>();

            using (var excelPackage = new ExcelPackage(excelStream))
            {
                var worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new ArgumentException("Excel file is empty or not properly formatted.");

                var rowCount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowCount; row++) // Assuming header row is skipped
                {
                    var product = new Product
                    {
                        Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                        Name = worksheet.Cells[row, 2].Value.ToString(),
                        Description = worksheet.Cells[row, 3].Value.ToString(),
                        Price = decimal.Parse(worksheet.Cells[row, 4].Value.ToString()),
                        ImageUrl = worksheet.Cells[row, 5].Value.ToString()
                    };
                    products.Add(product);
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
    }
}
