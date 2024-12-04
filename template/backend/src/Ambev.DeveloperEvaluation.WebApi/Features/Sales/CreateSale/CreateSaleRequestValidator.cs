using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumber).GreaterThan(0);
            RuleFor(x => x.SaleDate).NotEmpty();
            //RuleFor(x => x.Customer).NotNull();
            //RuleFor(x => x.TotalAmount).GreaterThan(0);
        }
    }

}
