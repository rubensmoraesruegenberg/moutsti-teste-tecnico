using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale operation
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.Id));
        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems))
            .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdCustomer));

        CreateMap<SaleItemComand, SaleItem>();
    }
}
