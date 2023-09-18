using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Server.Controllers;
using Server.Handlers;
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
        var sut = new NiceHashController(fakeNiceHashHandler.Object, Mock.Of<ILogger<NiceHashController>>());
        
        // Act
        var result = await sut.Get();
        
        // Assert
        Assert.True(false);
    }
}