using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - IdBranch: The value of IdBranch must be a valid GUID.
        /// - IdCustomer: The value of IdCustomer must be a valid GUID.
        /// - TotalAmount: The value of TotalAmount must be greater than 0.
        /// - SaleItems: The list of SaleItems cannot be empty.
        /// </remarks>
        public CreateSaleCommandValidator()
        {
            RuleFor(sale => sale.IdBranch).NotEmpty();
            RuleFor(sale => sale.IdCustomer)
                .NotEmpty()
                .WithMessage("The value of IdCustomer must be greater than 0.")
                .Must(id => Guid.TryParse(id.ToString(), out _))
                .WithMessage("The value of IdCustomer must be a valid GUID.");

            RuleFor(sale => sale.SaleItems)
                .NotEmpty()
                .WithMessage("The list of SaleItems cannot be empty.");

        }
    }
}
