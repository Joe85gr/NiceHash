using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Server.Builders;
using Server.Handlers;
using ServerTests.Configuration;
using Xunit;

namespace ServerTests.HandlersTests;

public class NiceHashHandlerTests
{
    private readonly Mock<INiceHashDataBuilder> _mockOrchestrator;

    public NiceHashHandlerTests()
    {
        _mockOrchestrator = new Mock<INiceHashDataBuilder>();
        Helper.ConfigureFakeEnvironmentalVariables();
    }

    [Fact]
    public async Task NiceHashHandler_ReturnsNiceHashData()
    {
        // Arrange
        var niceHashData = FakeData.FakeNiceHashData();
        _mockOrchestrator.Setup(x =>
                x.GetNiceHashData(It.IsAny<CancellationToken>()))
            .ReturnsAsync(niceHashData);
        // Act
        var handler = new NiceHashHandler(_mockOrchestrator.Object);
        var cancellationToken = new CancellationToken();
        var result = await handler.Handle(cancellationToken);
        // Assert
        result.Should().BeEquivalentTo(niceHashData);
        _mockOrchestrator.Verify(x => x.GetNiceHashData(cancellationToken), Times.Once);
    }
}