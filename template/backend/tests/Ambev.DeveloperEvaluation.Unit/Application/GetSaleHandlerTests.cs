using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
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
    /// Contains unit tests for the <see cref="GetSaleHandler"/> class.
    /// </summary>
    public class GetSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public GetSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetSaleHandler(_saleRepository, _mapper);
        }

        /// <summary>
        /// Tests that a valid get sale request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid request When getting sale Then returns sale details")]
        public async Task Handle_ValidRequest_ReturnsSaleDetails()
        {
            // Given
            var command = new GetSaleCommand(Guid.NewGuid());
            var sale = new Sale { Id = command.Id, SaleDate = DateTime.Now, TotalAmount = 100, IsCancelled = false, IdBranch = Guid.NewGuid(), Status = SaleStatus.Active, IdUser = Guid.NewGuid() };
            var saleResult = new GetSaleResult { Id = sale.Id, SaleDate = sale.SaleDate, TotalAmount = sale.TotalAmount, IsCancelled = sale.IsCancelled, IdBranch = sale.IdBranch, Status = sale.Status, IdUser = sale.IdUser };

            _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            _mapper.Map<GetSaleResult>(Arg.Any<Sale>())
                .Returns(saleResult);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(sale.Id);
            result.TotalAmount.Should().Be(sale.TotalAmount);
            await _saleRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that GetSaleHandler handles the request with a non-existent sale correctly.
        /// </summary>
        [Fact(DisplayName = "Given valid request When sale not found Then throws KeyNotFoundException")]
        public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
        {
            // Given
            var command = new GetSaleCommand(Guid.NewGuid());

            _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Sale>(null));

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.Id} not found");
        }

        /// <summary>
        /// Tests that GetSaleHandler handles the request with an invalid command correctly.
        /// </summary>
        [Fact(DisplayName = "Given invalid request When handling Then throws ValidationException")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new GetSaleCommand(Guid.Empty); // Invalid command with empty Guid

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
