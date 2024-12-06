using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    /// <summary>
    /// Handler for processing GetSalesCommand requests
    /// </summary>
    public class GetSalesHandler : IRequestHandler<GetSalesCommand, GetSalesPaginatedResult<GetSalesResult>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetSalesHandler
        /// </summary>
        /// <param name="saleRepository">The Sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public GetSalesHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }


        public async Task<GetSalesPaginatedResult<GetSalesResult>> Handle(GetSalesCommand command, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetSalesAsync(command.PageNumber, command.PageSize, cancellationToken);
            var totalSales = await _saleRepository.GetSalesCountAsync(cancellationToken);

            var salesResponse = _mapper.Map<List<GetSalesResult>>(sales.Where(c => c.IsCancelled == false));

            return new GetSalesPaginatedResult<GetSalesResult>
            {
                Data = salesResponse,
                CurrentPage = command.PageNumber,
                TotalPages = (int)Math.Ceiling(totalSales / (double)command.PageSize),
                TotalCount = totalSales
            };
        }
    }


}
