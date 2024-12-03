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
            //RuleFor(sale => sale.IdBranch).GreaterThan(0).WithMessage("O valor de IdBranch deve ser maior que 0.");
            RuleFor(sale => sale.IdBranch).GreaterThan(0);
            RuleFor(sale => sale.IdCustomer) .GreaterThan(0) .WithMessage("O valor de IdCustomer deve ser maior que 0.");
            RuleFor(sale => sale.TotalAmount).GreaterThan(0).WithMessage("O valor de TotalAmount deve ser maior que 0."); 
            RuleFor(sale => sale.SaleItems) .NotEmpty() .WithMessage("A lista de SaleItems não pode estar vazia.");

        }
    }
}
