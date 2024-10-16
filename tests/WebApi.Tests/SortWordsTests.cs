using WebApi.Controllers;
using WebApi.Taxes;
using WebApi.WordProcessing;

namespace WebApi.Tests;

public class SortWordsTests
{
    private readonly ApiController _controller;
    
    public SortWordsTests()
    {
        _controller = new ApiController(new TextProcessingService(), new TaxCalculator(new TaxBracketsLoader()));
    }

    [Fact]
    public void HelloThereTest()
    {
        var result = _controller.SortWords("Hello there!");

        Assert.Equal("eHllo eehrt!", result);
    }

    [Fact]
    public void LxTest()
    {
        var result = _controller.SortWords("LX's head office is located in Sydney, Australia.");

        Assert.Equal("'LsX adeh ceffio is acdelot in denSyy, Aaailrstu.", result);
    }

    [Fact]
    public void HelloWorldTest()
    {
        var result = _controller.SortWords("\"Hello World!\"");

        Assert.Equal("\"eHllo dlorW!\"", result);
    }

    [Fact]
    public void ComplexTest()
    {
        var result = _controller.SortWords("\"\"Very,complex , one test,.With multiple\", unexpected!!\",.chare-ters...");

        Assert.Equal("\"\"erVy,celmopx , eno estt,.hitW eillmptu\", cdeeenptux!!\",.acehr-erst...", result);
    }
}

