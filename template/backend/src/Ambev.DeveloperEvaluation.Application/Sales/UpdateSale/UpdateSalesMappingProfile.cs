using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
   

    public class UpdateSalesMappingProfile : Profile
    {
        public UpdateSalesMappingProfile()
        {
            CreateMap<Sale, UpdateSaleResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled))
                .ForMember(dest => dest.IdBranch, opt => opt.MapFrom(src => src.IdBranch))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser));
        }
    }

}
