using Microsoft.EntityFrameworkCore;
using pdfreadquery.Models;

namespace pdfreadquery
{
    public class PdfDbContext : DbContext
    {
        public PdfDbContext(DbContextOptions<PdfDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<BankInfo> BankInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<BankInfo>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }

}
