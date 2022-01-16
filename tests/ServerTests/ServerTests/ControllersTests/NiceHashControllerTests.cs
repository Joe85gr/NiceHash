using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using Server.Queries;
using Xunit;

namespace ServerTests.ControllersTests;

public class NiceHashControllerTests
{
    private Mock<IMediator> _mockMediator;

    public NiceHashControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
    }
    
    [Fact]
    public async Task Get_ReturnsNiceHashData_And_QueryIsCalledOnce()
    {
        // Arrange
        var niceHashData = new NiceHashData();
        _mockMediator
            .Setup(x => x.Send(It.IsAny<NiceHashQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(niceHashData);

        // Act
        var controller = new NiceHashController(_mockMediator.Object);
        var response = await controller.Get();

        // Assert
        var result = Assert.IsType<OkObjectResult>(response);
        Assert.IsType<NiceHashData>(result.Value);
        _mockMediator.Verify(x => x.Send(It.IsAny<NiceHashQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}