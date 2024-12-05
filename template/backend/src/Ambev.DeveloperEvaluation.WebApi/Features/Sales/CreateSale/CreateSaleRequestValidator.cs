using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{

    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(request => request.SaleDate).NotEmpty().WithMessage("Sale date is required.");
            RuleFor(request => request.IdCustomer).NotEmpty().WithMessage("Customer ID is required.");
            RuleFor(request => request.IdBranch).NotEmpty().WithMessage("Branch ID is required.");

            RuleFor(request => request.SaleItems)
                .NotEmpty().WithMessage("The list of sale items cannot be empty.")
                .Must(items => items.Count > 0).WithMessage("The quantity of items must be greater than 0.");

            RuleForEach(request => request.SaleItems).SetValidator(new SaleItemDtoValidator());
        }
    }

    public class SaleItemDtoValidator : AbstractValidator<CreateSaleRequest.SaleItemRequest>
    {
        public SaleItemDtoValidator()
        {
            RuleFor(item => item.IdProduct).NotEmpty().WithMessage("Product ID must be greater than 0.");
            RuleFor(item => item.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            RuleFor(item => item.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than 0.");
            
        }
    }


}
