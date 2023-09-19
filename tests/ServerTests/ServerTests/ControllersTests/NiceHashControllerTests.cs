using System.Threading.Tasks;
using Domain.Handlers;
using Microsoft.Extensions.Logging;
using Moq;
using Server.Api;
using ServerTests.Configuration;
using Xunit;

namespace ServerTests.ControllersTests;

public class NiceHashControllerTests
{
    
    [Fact]
    public async Task Get_ReturnsNiceHashData_And_QueryIsCalledOnce()
    {
        // Arrange
        Helper.ConfigureFakeEnvironmentalVariables();
        var fakeNiceHashHandler = new Mock<INiceHashHandler>();
        var sut = new ActivityController(fakeNiceHashHandler.Object, Mock.Of<ILogger<ActivityController>>());
        
        // Act
        var result = await sut.Get();
        
        // Assert
        Assert.True(false);
    }
}