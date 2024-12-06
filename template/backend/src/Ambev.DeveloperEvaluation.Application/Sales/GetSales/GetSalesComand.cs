using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSalesCommand : IRequest<GetSalesPaginatedResult<GetSalesResult>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetSalesCommand(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }


}


