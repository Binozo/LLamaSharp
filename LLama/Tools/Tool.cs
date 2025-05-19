using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LLama.Unittest.Tools;

namespace LLama.Tools;

public sealed class Tool
{
    public string Name { get; init; }

    public string Description { get; init; }
    
    public List<ToolParameter> Arguments { get; init; }

    private readonly Delegate _delegate;
    private readonly object? _contextClass;
    
    public Tool(Delegate callable)
    {
        // CamelCase to underscore_case
        var name = string.Concat(callable.Method.Name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        var description = callable.Method.Name;
        
        var toolCallAttribute = callable.Method.GetCustomAttribute<ToolCallAttribute>();
        if (toolCallAttribute != null)
        {
            if (toolCallAttribute.Name != null)
            {
                name = toolCallAttribute.Name;
            }
            description = toolCallAttribute.Description;
        }

        Name = name;
        Description = description;

        var parameters = new List<ToolParameter>();
        foreach (var parameter in callable.Method.GetParameters())
        {
            var parameterName = parameter.Name;
            var parameterDescription = parameterName;
            var parameterRequired = false;
            
            var parameterAttribute = parameter.GetCustomAttribute<ToolCallAttribute>();
            if (parameterAttribute != null)
            {
                if (parameterAttribute.Name != null)
                {
                    parameterName = parameterAttribute.Name;
                }
                parameterDescription = parameterAttribute.Description;
                parameterRequired = parameterAttribute.Required;
            }
            
            parameters.Add(new ToolParameter
            {
                Name = parameterName,
                Description = parameterDescription,
                Required = parameterRequired,
            });
        }

        _delegate = callable;
    }
    
    public Tool(Delegate callable, object? contextClass) : this(callable)
    {
        _contextClass = contextClass;
    }

    public Task<string> Invoke(object?[]? parameters)
    {
        return (Task<string>)_delegate.Method.Invoke(_contextClass, parameters);
    }
}