﻿using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{

    /// <summary>
    /// Gets or sets the date of the sale.
    /// </summary>
    public DateTime SaleDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the customer information.
    /// </summary>
    public Guid IdCustomer { get; set; } = Guid.Empty; // Atualizado para Guid

    /// <summary>
    /// Gets or sets the branch information.
    /// </summary>
    public Guid IdBranch { get; set; } // Atualizado para Guid

    /// <summary>
    /// Gets or sets the list of sale items.
    /// </summary>
    public List<SaleItemComand> SaleItems { get; set; } = new List<SaleItemComand>(); // Usar Dto para SaleItems

    /// <summary>
    /// Gets or sets the Cancelled information.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Validates the create sale command.
    /// </summary>
    /// <returns>A validation result detail object.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

public class SaleItemComand
{
    public Guid IdProduct { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}
