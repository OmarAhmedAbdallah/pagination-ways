namespace PaginationWays.Entities;

public class Invoice
{
    public int Id { get; set; }
    public string InvoiceID { get; set; }
    public char? Branch { get; set; }
    public string City { get; set; }
    public string CustomerType { get; set; }
    public string Gender { get; set; }
    public string ProductLine { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? Quantity { get; set; }
    public decimal? Tax5Percent { get; set; }
    public decimal? Total { get; set; }
    public DateTime? Date { get; set; }
    public TimeSpan? Time { get; set; }
    public string Payment { get; set; }
    public decimal? COGS { get; set; }
    public decimal? GrossMarginPercentage { get; set; }
    public decimal? GrossIncome { get; set; }
    public decimal? Rating { get; set; }
}