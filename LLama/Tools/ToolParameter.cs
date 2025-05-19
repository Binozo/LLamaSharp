namespace LLama.Unittest.Tools;

public sealed class ToolParameter
{
    public string Name { get; init; }
    public string Description { get; init; }
    public bool Required { get; init; }
    // TODO: Allowed values
}