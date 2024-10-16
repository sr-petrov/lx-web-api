using WebApi.Controllers;
using WebApi.TaxProcessing;
using WebApi.TaxProcessing.Calculators;
using WebApi.WordProcessing;

namespace WebApi.Tests;

public class ReverseWordsTests
{
    private readonly ApiController _controller;

    public ReverseWordsTests()
    {
        var textProcessingService = new TextProcessingService();
        var taxCalculator = new TaxCalculator(new TaxBracketsLoader(), new MedicareTaxCalculator());

        _controller = new ApiController(textProcessingService, taxCalculator);
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
