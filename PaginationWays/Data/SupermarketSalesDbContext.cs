using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using PaginationWays.Entities;

namespace PaginationWays.Data;

public class SupermarketSalesDbContext: DbContext
{
    private const int Cursor = 1;
    public DbSet<Invoice> Invoices { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=./;Initial Catalog=SupermarketSales;Integrated security=True;TrustServerCertificate=Yes;");
    }
    
    [Benchmark]
    [Arguments(1, 500)]
    public async Task<object> GetInvoicesWithSkipTakePaging(int pageNumber, int pageSize)
    {
        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than or equal to 1.");

        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than or equal to 1.");

        // Calculate the number of items to skip
        int itemsToSkip = (pageNumber - 1) * pageSize;

        // Perform the paged query
        var page = await Invoices // You can change the sorting criteria based on your needs
            .Skip(itemsToSkip)
            .Take(pageSize)
            .ToListAsync();

        var nextPage = await Invoices.CountAsync();

        return (page, nextPage);
    }
    
    [Benchmark]
    [Arguments(500)]
    public async Task<object> GetInvoicesWithCursorPaging(int pageSize)
    {
        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than or equal to 1.");

        
        // Perform the paged query
        var page = await Invoices// You can change the sorting criteria based on your needs
            .Where(x => x.Id > 499)
            .Take(pageSize)
            .ToListAsync();
        
        var nextCursor = page[^1].Id;
        return (page, nextCursor);
    }
}   