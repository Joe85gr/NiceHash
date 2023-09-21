using Domain.Encryption;
using FluentAssertions;
using Infrastructure.Builders;
using TestHelpers;

namespace InfrastructureTests.BuildersTests;

public class RequestBuilderTests
{
    [Fact]
    public void RequestIsBuilt()
    {
        // Arrange
        Helper.ConfigureFakeEnvironmentalVariables();
        const string serverTime = "123456";
        const string baseUrl = "https://api.nicehash.com";
        const string endpoint = "main/api/v2/mining/rigs2";
        var guid = new Guid().ToString();
        var method = HttpMethod.Get;
        var hashStructure = new HashStructure(serverTime, endpoint, method, guid);

        var expectedUri = new Uri(baseUrl + endpoint);
        const string expectedXAuth = "some-api-key:7ed687e18acfeb1cc0fd47a9b0b11ca2ff908d979e048bc7585820d7d5bc65a2";
        
        // Act
        var sut = new RequestBuilder(baseUrl, endpoint)
            .WithHeaders(serverTime, hashStructure)
            .WithMethod(method)
            .Build();

        // Assert
        sut.RequestUri.Should().BeEquivalentTo(expectedUri);
        sut.Headers.GetValues("X-Auth").First().Should().BeEquivalentTo(expectedXAuth);
        sut.Headers.GetValues("X-Nonce").First().Should().BeEquivalentTo(hashStructure.Nonce);
        sut.Headers.GetValues("X-Time").First().Should().BeEquivalentTo(serverTime);
        sut.Headers.GetValues("X-Organization-Id").First().Should().BeEquivalentTo(hashStructure.OrgId);
    }
}