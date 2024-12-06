using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Contains unit tests for the <see cref="UpdateSaleHandler"/> class.
    /// </summary>
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly UpdateSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateSaleHandler(_saleRepository, _mapper);
        }

        /// <summary>
        /// Tests that a valid update sale request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid request When updating sale Then returns updated sale details")]
        public async Task Handle_ValidRequest_ReturnsUpdatedSaleDetails()
        {
            // Given
            var command = new UpdateSaleCommand
            {
                Id = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                TotalAmount = 2000,
                IsCancelled = false,
                IdBranch = Guid.NewGuid(),
                Status = SaleStatus.Active,
                IdUser = Guid.NewGuid()
            };
            var sale = new Sale
            {
                Id = command.Id,
                SaleDate = command.SaleDate,
                TotalAmount = command.TotalAmount,
                IsCancelled = command.IsCancelled,
                IdBranch = command.IdBranch,
                Status = command.Status,
                IdUser = command.IdUser
            };
            var saleResult = new UpdateSaleResult
            {
                Id = sale.Id,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                IsCancelled = sale.IsCancelled,
                IdBranch = sale.IdBranch,
                Status = sale.Status,
                IdUser = sale.IdUser
            };

            _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(Task.CompletedTask);

            _mapper.Map<UpdateSaleResult>(Arg.Any<Sale>())
                .Returns(saleResult);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(sale.Id);
            result.TotalAmount.Should().Be(sale.TotalAmount);
            await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that UpdateSaleHandler handles the request with a non-existent sale correctly.
        /// </summary>
        [Fact(DisplayName = "Given valid request When sale not found Then throws KeyNotFoundException")]
        public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
        {
            // Given
            var command = new UpdateSaleCommand
            {
                Id = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                TotalAmount = 2000,
                IsCancelled = false,
                IdBranch = Guid.NewGuid(),
                Status = SaleStatus.Active,
                IdUser = Guid.NewGuid()
            };

            _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Sale>(null));

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.Id} not found");
        }

        
    }
}
