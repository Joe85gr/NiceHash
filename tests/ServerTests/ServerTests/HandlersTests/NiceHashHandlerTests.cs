using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Library.Models;
using Moq;
using Server.Handlers;
using Server.Orchestrators;
using Server.Queries;
using ServerTests.Configuration;
using Xunit;

namespace ServerTests.HandlersTests;

public class NiceHashHandlerTests
{
    private readonly Mock<INiceHashDataOrchestrator> _mockOrchestrator;

    public NiceHashHandlerTests()
    {
        _mockOrchestrator = new Mock<INiceHashDataOrchestrator>();
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
        var request = new NiceHashQuery();
        var handler = new NiceHashHandler(_mockOrchestrator.Object);
        var cancellationToken = new CancellationToken();
        var result = await handler.Handle(request, cancellationToken);
        // Assert
        result.Should().BeEquivalentTo(niceHashData);
        _mockOrchestrator.Verify(x => x.GetNiceHashData(cancellationToken), Times.Once);
    }
}