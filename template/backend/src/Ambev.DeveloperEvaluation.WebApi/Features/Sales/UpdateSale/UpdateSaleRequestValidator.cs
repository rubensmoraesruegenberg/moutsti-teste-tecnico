using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Sale ID is required.");
            RuleFor(x => x.SaleDate).NotEmpty().WithMessage("Sale date is required.");
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("Total amount must be greater than zero.");
            RuleFor(x => x.IdBranch).NotEmpty().WithMessage("Branch ID is required.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Status must be a valid value.");
            RuleFor(x => x.IdUser).NotEmpty().WithMessage("User ID is required.");
        }
    }

}
