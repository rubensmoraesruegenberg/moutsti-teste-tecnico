using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    internal class GetSaleMappingProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetUser operation
        /// </summary>
        public GetSaleMappingProfile()
        {
            CreateMap<Sale, GetSaleResult>();
        }
    }
}