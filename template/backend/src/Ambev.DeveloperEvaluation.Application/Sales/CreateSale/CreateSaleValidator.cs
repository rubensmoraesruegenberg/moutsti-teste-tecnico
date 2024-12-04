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
        /// - IdBranch: O valor de IdCustomer deve ser maior que 0.
        /// - IdCustomer: O valor de TotalAmount deve ser maior que 0.
        /// - TotalAmount: Must meet security requirements (using PasswordValidator)
        /// - SaleItems: A lista de SaleItems não pode estar vazia.
        /// </remarks>
        public CreateSaleCommandValidator()
        {
            RuleFor(sale => sale.IdBranch).GreaterThan(0);
            RuleFor(sale => sale.IdCustomer)
                .NotEmpty()
                .WithMessage("The value of IdCustomer must be greater than 0.")
                .Must(id => Guid.TryParse(id.ToString(), out _))
                .WithMessage("The value of IdCustomer must be a valid GUID.");

            RuleFor(sale => sale.TotalAmount)
                .GreaterThan(0)
                .WithMessage("User ID is required.");
            RuleFor(sale => sale.SaleItems)
                .NotEmpty()
                .WithMessage("The list of SaleItems cannot be empty.");

        }
    }
}
