using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSale
{
    public class SaleResponse
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public Guid IdBranch { get; set; }
        public SaleStatus Status { get; set; }
        public Guid IdUser { get; set; }
    }
}
