using Library.Models;
using Server.Encryption;
using Xunit;

namespace ServerTests.EncryptionTests;

public class Sha256EncryptionTests
{
    [Fact]
    public void GenerateTextToHash_ParsesTextCorrectly()
    {
        // Arrange
        const string time = "12345678";
        const string endpoint = "FancyEndpoint?fancyParameter=fancyValue";
        const string guid = "some_guid";
        var hashStructure = new HashStructure(time, endpoint, RequestMethod.GET, guid);
        // Act
        const string expectedText = " 12345678 some_guid    GET FancyEndpoint fancyParameter=fancyValue";
        var actualText = Sha256Encryption.GenerateTextToHash(hashStructure);
        // Assess
        Assert.Equal(expectedText, actualText);
    }
    
    [Fact]
    public void GenerateHash_ParsesTextCorrectly()
    {
        // Arrange
        const string text = " 12345678 some_guid    GET FancyEndpoint fancyParameter=fancyValue";
        const string key = "some_fancy_key";
        // Act
        const string expectedHash = "127d2a27d6bdf794dfe1a11ce7c90efb46432bf76f04af5d4df29057079120c2";
        var actualHash = Sha256Encryption.GenerateHash(text, key);
        // Assess
        Assert.Equal(expectedHash, actualHash);
    }
}