using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Controllers;

namespace WebApi.Tests;

public class SortWordsTests
{
    [Fact]
    public void HelloThereTest()
    {
        var mockLogger = new Mock<ILogger<ApiController>>();

        var controller = new ApiController(mockLogger.Object);

        var result = controller.SortWords("Hello there!");

        Assert.Equal("eHllo eehrt!", result);
    }

    [Fact]
    public void LxTest()
    {
        var mockLogger = new Mock<ILogger<ApiController>>();

        var controller = new ApiController(mockLogger.Object);

        var result = controller.SortWords("LX's head office is located in Sydney, Australia.");

        Assert.Equal("'LsX adeh ceffio is acdelot in denSyy, Aaailrstu.", result);
    }

    [Fact]
    public void HelloWorldTest()
    {
        var mockLogger = new Mock<ILogger<ApiController>>();

        var controller = new ApiController(mockLogger.Object);

        var result = controller.SortWords("\"Hello World!\"");

        Assert.Equal("\"eHllo dlorW!\"", result);
    }

    [Fact]
    public void ComplexTest()
    {
        var mockLogger = new Mock<ILogger<ApiController>>();

        var controller = new ApiController(mockLogger.Object);

        var result = controller.SortWords("\"\"Very,complex , one test,.With multiple\", unexpected!!\",.chare-ters...");

        Assert.Equal("\"\"erVy,celmopx , eno estt,.hitW eillmptu\", cdeeenptux!!\",.acehr-erst...", result);
    }
}

