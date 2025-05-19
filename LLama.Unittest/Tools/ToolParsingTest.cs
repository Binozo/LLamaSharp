using LLama.Tools;

namespace LLama.Unittest.Tools;

public class ToolParsingTest
{
    [Fact]
    public void ParseToolFromMethod()
    {
        var tool = new Tool(GetStockPrice, this);
        
        Assert.Equal("get_stock_price", tool.Name);
        Assert.Equal("Gets the current stock price for the given symbol", tool.Description);
        
        Assert.Single(tool.Arguments);
        Assert.Equal("The stock symbol name to get the current price for", tool.Arguments[0].Name);
        Assert.True(tool.Arguments[0].Required);
    }

    [ToolCall("Gets the current stock price for the given symbol")]
    private string GetStockPrice([ToolCallArgument("The stock symbol name to get the current price for", true)] string symbol)
    {
        return "100$";
    }
}