using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public DateTime SaleDate { get; set; }
        public Guid IdCustomer { get; set; }
        public Guid IdBranch { get; set; }
        public bool IsCancelled { get; set; } = false;
        public List<SaleItemRequest> SaleItems { get; set; } 

        public class SaleItemRequest
        {
            public Guid IdProduct { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            
        }
    }

}
