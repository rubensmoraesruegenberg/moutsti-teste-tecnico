using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Entities;

/// <summary>
/// Contains unit tests for the <see cref="Sale"/> entity.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that discounts are applied correctly based on item quantities.
    /// </summary>
    [Fact(DisplayName = "Given sale items When applying discount Then correct discounts are applied")]
    public void ApplyDiscount_AppliesCorrectDiscounts()
    {
        // Given
        var sale = SaleTestData.GenerateValidSale().Generate();
        sale.SaleItems = new List<SaleItem>
        {
            SaleTestData.GenerateValidSaleItem(5).Generate(), // 10% discount
            SaleTestData.GenerateValidSaleItem(15).Generate() // 20% discount
        };

        // When
        sale.ApplyDiscount();

        // Then
        sale.SaleItems[0].Discount.Should().Be(0.10m);
        sale.SaleItems[1].Discount.Should().Be(0.20m);
        sale.SaleItems[0].TotalAmount.Should().Be(sale.SaleItems[0].Quantity * sale.SaleItems[0].UnitPrice * 0.90m);
        sale.SaleItems[1].TotalAmount.Should().Be(sale.SaleItems[1].Quantity * sale.SaleItems[1].UnitPrice * 0.80m);
    }

    /// <summary>
    /// Tests that an exception is thrown when selling more than 20 identical items.
    /// </summary>
    [Fact(DisplayName = "Given more than 20 identical items When applying discount Then throws exception")]
    public void ApplyDiscount_ThrowsExceptionForExcessiveQuantity()
    {
        // Given
        var sale = SaleTestData.GenerateValidSale().Generate();
        sale.SaleItems = new List<SaleItem>
        {
            SaleTestData.GenerateValidSaleItem(25).Generate() // More than 20 items
        };

        // When
        Action act = () => sale.ApplyDiscount();

        // Then
        act.Should().Throw<InvalidOperationException>().WithMessage("Não é possível vender acima de 20 itens idênticos.");
    }

    /// <summary>
    /// Tests that no discount is applied for less than 4 items.
    /// </summary>
    [Fact(DisplayName = "Given less than 4 items When applying discount Then no discount is applied")]
    public void ApplyDiscount_NoDiscountForLessThanFourItems()
    {
        // Given
        var sale = SaleTestData.GenerateValidSale().Generate();
        sale.SaleItems = new List<SaleItem>
        {
            SaleTestData.GenerateValidSaleItem(3).Generate() // Less than 4 items
        };

        // When
        sale.ApplyDiscount();

        // Then
        sale.SaleItems[0].Discount.Should().Be(0m);
        sale.SaleItems[0].TotalAmount.Should().Be(sale.SaleItems[0].Quantity * sale.SaleItems[0].UnitPrice);
    }
}
