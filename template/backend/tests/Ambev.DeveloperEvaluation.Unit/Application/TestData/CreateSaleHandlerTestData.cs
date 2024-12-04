using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

public static class CreateSaleHandlerTestData
{
    public static Faker<CreateSaleCommand> GenerateValidCommand()
    {
        return new Faker<CreateSaleCommand>()
            .RuleFor(c => c.SaleNumber, f => f.Random.Int(1, 1000))
            .RuleFor(c => c.SaleDate, f => f.Date.Past())
            .RuleFor(c => c.IdCustomer, f => f.Random.Guid())
            .RuleFor(c => c.TotalAmount, f => f.Finance.Amount())
            .RuleFor(c => c.IdBranch, f => f.Random.Int(1, 10))
            .RuleFor(c => c.SaleItems, f => new List<SaleItem>
            {
                new SaleItem { ProductId = f.Random.Int(1, 50), Quantity = f.Random.Int(1, 10), UnitPrice = f.Finance.Amount() }
            });
    }

    public static Faker<Sale> GenerateValidSale()
    {
        return new Faker<Sale>()
            .RuleFor(s => s.SaleNumber, f => f.Random.Int(1, 1000))
            .RuleFor(s => s.SaleDate, f => f.Date.Past())
            .RuleFor(s => s.CustomerId, f => f.Random.Int(1, 100))
            .RuleFor(s => s.TotalAmount, f => f.Finance.Amount())
            .RuleFor(s => s.BranchId, f => f.Random.Guid())
            .RuleFor(s => s.SaleItems, f => new List<SaleItem>
            {
                new SaleItem { ProductId = f.Random.Int(1, 50), Quantity = f.Random.Int(1, 10), UnitPrice = f.Finance.Amount() }
            });
    }
}
