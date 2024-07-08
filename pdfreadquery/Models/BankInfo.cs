namespace pdfreadquery.Models
{
    public class BankInfo
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string InstitutionName { get; set; }
        public string PhysicalAddress1 { get; set; }
        public string PhysicalAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryName { get; set; }
        public string SwiftCode { get; set; }
    }
}
