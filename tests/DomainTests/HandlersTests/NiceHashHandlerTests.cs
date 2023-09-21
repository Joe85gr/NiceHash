using Domain;
using Domain.Handlers;
using FluentAssertions;
using FluentResults;
using Library.Models;
using Moq;

namespace DomainTests.HandlersTests;

public class NiceHashHandlerTests
{
    private readonly NiceHashHandler _sut;
    private readonly Mock<IDataService> _dataServiceMock;
    
    private const string GenericErrorMessage = "Some error message";
    
    public NiceHashHandlerTests()
    {
        _dataServiceMock = new Mock<IDataService>();
        _sut = new NiceHashHandler(_dataServiceMock.Object);   
    }
    
    [Fact]
    public async Task ReturnsOkResult()
    {
        const string serverTime = "2021-10-10 10:10:10";
        
        // Arrange
        _dataServiceMock
            .Setup(mock => mock.GetServerTime(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(serverTime));

        var currency = FakeData.FakeCurrency();
        
        _dataServiceMock
            .Setup(mock => mock.GetBtcBalance(serverTime, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(currency));
        
        var rigs2 = FakeData.FakeRigs2();
        
        _dataServiceMock
            .Setup(mock => mock.GetRigsDetails(serverTime, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(rigs2));
        
        // Act
        var result = await _sut.Handle(It.IsAny<CancellationToken>());
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task WhenServerTimeResultIsFailed_ReturnsFailResult()
    {
        // Arrange
        const string errorMessage = "Invalid server time";
        var expectedResult = Result.Fail<RigsActivity>(errorMessage);
        
        _dataServiceMock
            .Setup(mock => mock.GetServerTime(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail<string>(errorMessage));
        
        // Act
        var result = await _sut.Handle(It.IsAny<CancellationToken>());
        
        // Assert
        result.Errors.Should().BeEquivalentTo(expectedResult.Errors);
        result.IsFailed.Should().BeTrue();
    }
    
    [Fact]
    public async Task WhenBalancesResultIsFailed_ReturnsFailResult()
    {
        const string serverTime = "2021-10-10 10:10:10";
        
        // Arrange
        _dataServiceMock
            .Setup(mock => mock.GetServerTime(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(serverTime));

        _dataServiceMock
            .Setup(mock => mock.GetBtcBalance(serverTime, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail<Currency>(GenericErrorMessage));

        
        // Act
        var result = await _sut.Handle(It.IsAny<CancellationToken>());
        
        // Assert
        result.IsFailed.Should().BeTrue();
    }
    
    [Fact]
    public async Task WhenRigsDetailsResultIsFailed_ReturnsFailResult()
    {
        const string serverTime = "2021-10-10 10:10:10";
        
        // Arrange
        _dataServiceMock
            .Setup(mock => mock.GetServerTime(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(serverTime));

        var currency = FakeData.FakeCurrency();
        
        _dataServiceMock
            .Setup(mock => mock.GetBtcBalance(serverTime, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(currency));
        
        _dataServiceMock
            .Setup(mock => mock.GetRigsDetails(serverTime, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail<Rigs2>(GenericErrorMessage));
        
        // Act
        var result = await _sut.Handle(It.IsAny<CancellationToken>());
        
        // Assert
        result.IsFailed.Should().BeTrue();
    }
}