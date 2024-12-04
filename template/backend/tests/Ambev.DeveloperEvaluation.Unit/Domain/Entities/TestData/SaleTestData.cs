using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

public static class SaleTestData
{
    public static Faker<Sale> GenerateValidSale()
    {
        return new Faker<Sale>()
            .RuleFor(s => s.SaleNumber, f => f.Random.Int(1, 1000))
            .RuleFor(s => s.SaleDate, f => f.Date.Past())
            .RuleFor(s => s.IdUser, f => f.Random.Guid())
            .RuleFor(s => s.TotalAmount, f => f.Finance.Amount())
            .RuleFor(s => s.SaleItems, f => new List<SaleItem>
            {
                new SaleItem { ProductId = f.Random.Int(1, 50), Quantity = f.Random.Int(1, 20), UnitPrice = f.Finance.Amount() }
            });
    }

    public static Faker<SaleItem> GenerateValidSaleItem(int quantity)
    {
        return new Faker<SaleItem>()
            .RuleFor(i => i.ProductId, f => f.Random.Int(1, 50))
            .RuleFor(i => i.Quantity, quantity)
            .RuleFor(i => i.UnitPrice, f => f.Finance.Amount());
    }
}
