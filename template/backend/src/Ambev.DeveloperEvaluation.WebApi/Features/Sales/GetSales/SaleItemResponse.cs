namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    public class SaleItemResponse
    {
        public Guid IdProduct { get; set; } // Identificador do Produto
        public int Quantity { get; set; } // Quantidade do Produto
        public decimal UnitPrice { get; set; } // Preço Unitário do Produto
    }

}
