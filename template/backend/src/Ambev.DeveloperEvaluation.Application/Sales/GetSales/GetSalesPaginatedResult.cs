namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSalesPaginatedResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }


}
