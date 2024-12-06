using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Contains unit tests for the <see cref="GetSalesHandler"/> class.
    /// </summary>
    public class GetSalesHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetSalesHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public GetSalesHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetSalesHandler(_saleRepository, _mapper);
        }

        /// <summary>
        /// Tests that a valid get sales request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid request When getting sales Then returns paginated sales")]
        public async Task Handle_ValidRequest_ReturnsPaginatedSales()
        {
            // Given
            var command = new GetSalesCommand(1, 10); // Passando os parâmetros exigidos
            var sales = new List<Sale>
            {
                new Sale { Id = Guid.NewGuid(), SaleDate = DateTime.Now, TotalAmount = 100, IsCancelled = false, IdBranch = Guid.NewGuid(), Status = SaleStatus.Active, IdUser = Guid.NewGuid() }
            };
            var salesResults = new List<GetSalesResult>
            {
                new GetSalesResult { Id = sales[0].Id, SaleDate = sales[0].SaleDate, TotalAmount = sales[0].TotalAmount, IsCancelled = sales[0].IsCancelled, IdBranch = sales[0].IdBranch, Status = sales[0].Status, IdUser = sales[0].IdUser }
            };

            _saleRepository.GetSalesAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<Sale>)sales));

            _saleRepository.GetSalesCountAsync(Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(1));

            _mapper.Map<List<GetSalesResult>>(Arg.Any<List<Sale>>())
                .Returns(salesResults);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Data.Should().HaveCount(1);
            result.CurrentPage.Should().Be(1);
            result.TotalPages.Should().Be(1);
            result.TotalCount.Should().Be(1);
        }

        /// <summary>
        /// Tests that GetSalesHandler handles the request with an empty result correctly.
        /// </summary>
        [Fact(DisplayName = "Given valid request When no sales found Then returns empty paginated result")]
        public async Task Handle_ValidRequest_ReturnsEmptyResult()
        {
            // Given
            var command = new GetSalesCommand(1, 10); // Passando os parâmetros exigidos

            _saleRepository.GetSalesAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<Sale>)new List<Sale>()));

            _saleRepository.GetSalesCountAsync(Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(0));

            _mapper.Map<List<GetSalesResult>>(Arg.Any<List<Sale>>())
                .Returns(new List<GetSalesResult>());

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Data.Should().BeEmpty();
            result.CurrentPage.Should().Be(1);
            result.TotalPages.Should().Be(0);
            result.TotalCount.Should().Be(0);
        }
    }
}
