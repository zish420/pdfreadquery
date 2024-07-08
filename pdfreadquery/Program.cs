using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;
using pdfreadquery;
using pdfreadquery.Service.Abstraction;
using pdfreadquery.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
builder.Services.AddDbContext<PdfDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBankInfoRepository, BankInfoRepository>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Pdf", Version = "v1" });
});

builder.Services.AddScoped<IPdfProcessor, PdfProcessor>();
builder.Services.AddScoped<IExcelProcessor, ExcelProcessor>();
builder.Services.AddScoped<IExcelService, ExcelService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pdf v1");
    options.RoutePrefix = "swagger"; // Or any other route prefix you prefer
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

