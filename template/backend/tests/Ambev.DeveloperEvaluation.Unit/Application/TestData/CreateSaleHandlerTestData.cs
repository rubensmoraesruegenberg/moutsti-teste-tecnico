using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

public static class CreateSaleHandlerTestData
{
    public static Faker<CreateSaleCommand> GenerateValidCommand()
    {
        return new Faker<CreateSaleCommand>()
            .RuleFor(c => c.SaleDate, f => f.Date.Past())
            .RuleFor(c => c.IdCustomer, f => f.Random.Guid())
            .RuleFor(c => c.IdBranch, f => f.Random.Guid())
            .RuleFor(c => c.IsCancelled, f => f.Random.Bool())
            .RuleFor(c => c.SaleItems, f => new List<SaleItemComand>
            {
                new SaleItemComand
                {
                    IdProduct = f.Random.Guid(),
                    Quantity = f.Random.Int(1, 10),
                    UnitPrice = f.Finance.Amount(min: 1, max: 100),
                    Discount = f.Random.Decimal(0, 0.5m),
                    TotalAmount = f.Finance.Amount(min: 1, max: 100)
                }
            });

    }

    public static Faker<Sale> GenerateValidSale()
    {
        return new Faker<Sale>()
            .RuleFor(s => s.Id, f =>  f.Random.Guid())
            .RuleFor(s => s.SaleDate, f => f.Date.Past())
            .RuleFor(s => s.IdUser, f => f.Random.Guid())
            .RuleFor(s => s.TotalAmount, f => f.Finance.Amount())
            .RuleFor(s => s.IdBranch, f => f.Random.Guid())
            .RuleFor(s => s.SaleItems, f => new List<SaleItem>
            {
                new SaleItem { ProductId = f.Random.Int(1, 50), Quantity = f.Random.Int(1, 10), UnitPrice = f.Finance.Amount() }
            });
    }
}
