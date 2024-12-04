using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new sale.
    /// </summary>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        /// <summary>
        /// Gets or sets the sale number.
        /// </summary>
        public int SaleNumber { get; set; } = 0;

        /// <summary>
        /// Gets or sets the date of the sale.
        /// </summary>
        public DateTime SaleDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the customer information.
        /// </summary>
        public Guid IdCustomer { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; } = 0;

        /// <summary>
        /// Gets or sets the branch information.
        /// </summary>
        public int IdBranch { get; set; }

        /// <summary>
        /// Gets or sets the list of sale items.
        /// </summary>
        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

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
}
