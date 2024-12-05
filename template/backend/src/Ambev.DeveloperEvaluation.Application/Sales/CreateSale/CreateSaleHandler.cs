using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net.NetworkInformation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handler for processing CreateSaleCommand requests
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateSaleHandler
        /// </summary>
        /// <param name="saleRepository">The Sale repository</param>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="validator">The validator for CreateSaleCommand</param>
        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IUserRepository userRepository)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingUser = await _userRepository.GetByIdAsync(command.IdCustomer, cancellationToken);

            if (existingUser is null || (existingUser != null && existingUser.Role != Domain.Enums.UserRole.Customer))
                throw new InvalidOperationException($"Invalid User");

            var sale = _mapper.Map<Sale>(command);

            sale.ApplyDiscount();
            sale.UpdateTotalAmount();
            sale.Status = SaleStatus.Active;

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            
            return result;
        }
    }
    
}
