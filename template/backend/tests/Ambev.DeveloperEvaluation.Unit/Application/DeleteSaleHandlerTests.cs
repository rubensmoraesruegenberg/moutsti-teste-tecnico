using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Contains unit tests for the <see cref="DeleteSaleHandler"/> class.
    /// </summary>
    public class DeleteSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DeleteSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSaleHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public DeleteSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new DeleteSaleHandler(_saleRepository);
        }

        /// <summary>
        /// Tests that a valid delete sale request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid request When deleting sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = new DeleteSaleCommand(Guid.NewGuid());
            var sale = new Sale { Id = command.Id, SaleDate = DateTime.UtcNow, TotalAmount = 2000, IsCancelled = false, IdBranch = Guid.NewGuid(), Status = SaleStatus.Active, IdUser = Guid.NewGuid() };

            _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(Task.CompletedTask);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that DeleteSaleHandler handles the request with a non-existent sale correctly.
        /// </summary>
        [Fact(DisplayName = "Given valid request When sale not found Then throws KeyNotFoundException")]
        public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
        {
            // Given
            var command = new DeleteSaleCommand(Guid.NewGuid());

            _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Sale>(null));

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.Id} not found");
        }

        /// <summary>
        /// Tests that DeleteSaleHandler handles the request with an invalid command correctly.
        /// </summary>
        [Fact(DisplayName = "Given invalid request When handling Then throws ValidationException")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new DeleteSaleCommand(Guid.Empty); // Invalid command with empty Guid

            var validator = new DeleteSaleValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                // When
                var act = () => _handler.Handle(command, CancellationToken.None);

                // Then
                await act.Should().ThrowAsync<ValidationException>();
            }
        }
    }
}
