using WebApi.Controllers;
using WebApi.Taxes;
using WebApi.WordProcessing;

namespace WebApi.Tests;

public class ReverseWordsTests
{
    private readonly ApiController _controller;

    public ReverseWordsTests()
    {
        _controller = new ApiController(new TextProcessingService(), new TaxCalculator(new TaxBracketsLoader()));
    }

    [Fact]
    public void HelloThereTest()
    {
        var result = _controller.ReverseWords("Hello there!");

        Assert.Equal("olleH ereht!", result);
    }

    [Fact]
    public void LxTest()
    {
        var result = _controller.ReverseWords("LX's head office is located in Sydney, Australia.");

        Assert.Equal("s'XL daeh eciffo si detacol ni yendyS, ailartsuA.", result);
    }

    [Fact]
    public void HelloWorldTest()
    {
        var result = _controller.ReverseWords("\"Hello World!\"");

        Assert.Equal("\"olleH dlroW!\"", result);
    }

    [Fact]
    public void ComplexTest()
    {
        var result = _controller.ReverseWords("\"\"Very,complex , one test,.With multiple\", unexpected!!\",.chare-ters...");

        Assert.Equal("\"\"yreV,xelpmoc , eno tset,.htiW elpitlum\", detcepxenu!!\",.erahc-sret...", result);
    }
}
