using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Controllers;

namespace WebApi.Tests;

public class FirstTest
{
    [Fact]
    public void TestGet()
    {
        var mockLogger = new Mock<ILogger<ApiController>>();

        var controller = new ApiController(mockLogger.Object);

        var result = controller.TestGet();

        Assert.Equal("OK", result);
    }
}