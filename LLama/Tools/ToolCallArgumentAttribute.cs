using System;

namespace LLama.Unittest.Tools;

[AttributeUsage(AttributeTargets.Parameter)]
public class ToolCallArgumentAttribute : Attribute
{
    public readonly string? Name;
    public readonly string Description;
    public readonly bool Required;
    // TODO: Available values in enum list

    public ToolCallArgumentAttribute(string description, bool required = false) : this(null, description, required)
    {
        
    }

    public ToolCallArgumentAttribute(string? name, string description, bool required = false)
    {
        Name = name;
        Description = description;
        Required = required;
    }
}