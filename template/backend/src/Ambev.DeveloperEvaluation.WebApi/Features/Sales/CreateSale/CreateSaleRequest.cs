using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid IdCustomer { get; set; }
        public Guid IdBranch { get; set; }
        public List<SaleItemDto> SaleItems { get; set; }

        public class SaleItemDto
        {
            public int IdProduct { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Discount { get; set; }
            public decimal TotalAmount { get; set; }
            public bool IsCancelled { get; set; }
        }
    }

}
