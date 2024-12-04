using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
    /// </summary>
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly CreateSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new CreateSaleHandler(_saleRepository, _mapper, _userRepository);
        }

        /// <summary>
        /// Tests that a valid sale creation request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand().Generate();
            var sale = CreateSaleHandlerTestData.GenerateValidSale().Generate();
            var user = new User
            {
                Id = command.IdCustomer,
                Role = UserRole.Customer, // Ensure role is Customer to pass validation
                Username = "ExistingUser",
                Email = "existinguser@example.com",
                Password = "Password@123",
                Status = UserStatus.Active
            };

            _userRepository.GetByIdAsync(command.IdCustomer, Arg.Any<CancellationToken>())
                .Returns(user);

            var result = new CreateSaleResult
            {
                SaleNumber = sale.SaleNumber
            };

            _mapper.Map<Sale>(command).Returns(sale);
            _mapper.Map<CreateSaleResult>(sale).Returns(result);
            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(sale);

            // When
            var createSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createSaleResult.Should().NotBeNull();
            createSaleResult.SaleNumber.Should().Be(sale.SaleNumber);
            await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that an invalid sale creation request throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new CreateSaleCommand(); // Empty command will fail validation

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        /// <summary>
        /// Tests that the mapper is called with the correct command.
        /// </summary>
        [Fact(DisplayName = "Given valid command When handling Then maps command to sale entity")]
        public async Task Handle_ValidRequest_MapsCommandToSale()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand().Generate();
            var sale = CreateSaleHandlerTestData.GenerateValidSale().Generate();
            var user = new User
            {
                Id = command.IdCustomer,
                Role = UserRole.Customer, // Ensure role is Customer to pass validation
                Username = "ExistingUser",
                Email = "existinguser@example.com",
                Password = "Password@123",
                Status = UserStatus.Active
            };

            _userRepository.GetByIdAsync(command.IdCustomer, Arg.Any<CancellationToken>())
                .Returns(user);

            _mapper.Map<Sale>(command).Returns(sale);
            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(sale);

            // When
            await _handler.Handle(command, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c =>
                c.SaleNumber == command.SaleNumber &&
                c.SaleDate == command.SaleDate &&
                c.IdCustomer == command.IdCustomer &&
                c.IdBranch == command.IdBranch));
        }

        /// <summary>
        /// Tests that an invalid IdCustomer throws an invalid operation exception.
        /// </summary>
        [Fact(DisplayName = "Given non-existent IdCustomer When handling Then throws invalid operation exception")]
        public async Task Handle_NonExistentIdCustomer_ThrowsInvalidOperationException()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand().Generate();
            command.IdCustomer = Guid.NewGuid(); // Non-existent IdCustomer for testing

            var commandUser = CreateUserHandlerTestData.GenerateValidCommand();
            var user = new User
            {
                Role = UserRole.Admin,
            };


            _userRepository.GetByIdAsync(command.IdCustomer, Arg.Any<CancellationToken>())
                .Returns(user);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Invalid User");
        }

        /// <summary>
        /// Tests that a valid IdCustomer creates a sale successfully.
        /// </summary>
        [Fact(DisplayName = "Given existent IdCustomer When handling Then creates sale successfully")]
        public async Task Handle_ExistentIdCustomer_CreatesSaleSuccessfully()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand().Generate();
            var user = new User
            {
                Id = command.IdCustomer,
                Role = UserRole.Customer, // Ensure role is Customer to pass validation
                Username = "ExistingUser",
                Email = "existinguser@example.com",
                Password = "Password@123",
                Status = UserStatus.Active
            };

            var sale = CreateSaleHandlerTestData.GenerateValidSale().Generate();

            var result = new CreateSaleResult
            {
                SaleNumber = sale.SaleNumber
            };

            _userRepository.GetByIdAsync(command.IdCustomer, Arg.Any<CancellationToken>())
                .Returns(user);

            _mapper.Map<Sale>(command).Returns(sale);
            _mapper.Map<CreateSaleResult>(sale).Returns(result);
            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(sale);

            // When
            var createSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createSaleResult.Should().NotBeNull();
            createSaleResult.SaleNumber.Should().Be(sale.SaleNumber);
            await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
            await _userRepository.Received(1).GetByIdAsync(command.IdCustomer, Arg.Any<CancellationToken>());
        }


    }
}
