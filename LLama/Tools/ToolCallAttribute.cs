using System;

namespace LLama.Unittest.Tools;

[AttributeUsage(AttributeTargets.Method)]
public class ToolCallAttribute : Attribute
{
    public readonly string? Name;
    public readonly string Description;
    public readonly bool Required;

    public ToolCallAttribute(string name, string description, bool required = false) : this(description, required)
    {
        Name = name;
    }

    public ToolCallAttribute(string description, bool required = false)
    {
        Description = description;
        Required = required;
    }
}