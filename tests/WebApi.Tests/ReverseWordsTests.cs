using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Controllers;

namespace WebApi.Tests;

public class ReverseWordsTests
{
    [Fact]
    public void HelloThereTest()
    {
        var mockLogger = new Mock<ILogger<ApiController>>();

        var controller = new ApiController(mockLogger.Object);

        var result = controller.ReverseWords("Hello there!");

        Assert.Equal("olleH ereht!", result);
    }

    [Fact]
    public void LxTest()
    {
        var mockLogger = new Mock<ILogger<ApiController>>();

        var controller = new ApiController(mockLogger.Object);

        var result = controller.ReverseWords("LX's head office is located in Sydney, Australia.");

        Assert.Equal("s'XL daeh eciffo si detacol ni yendyS, ailartsuA.", result);
    }

    [Fact]
    public void HelloWorldTest()
    {
        var mockLogger = new Mock<ILogger<ApiController>>();

        var controller = new ApiController(mockLogger.Object);

        var result = controller.ReverseWords("\"Hello World!\"");

        Assert.Equal("\"olleH dlroW!\"", result);
    }

    [Fact]
    public void ComplexTest()
    {
        var mockLogger = new Mock<ILogger<ApiController>>();

        var controller = new ApiController(mockLogger.Object);

        var result = controller.ReverseWords("\"\"Very,complex , one test,.With multiple\", unexpected!!\",.chare-ters...");

        Assert.Equal("\"\"yreV,xelpmoc , eno tset,.htiW elpitlum\", detcepxenu!!\",.erahc-sret...", result);
    }
}
