using System.Text.RegularExpressions;

namespace Catalog.API.Utils;

public class KebabNamingParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if(value == null) return null;
        string? name = value.ToString();
        if(string.IsNullOrEmpty(name)) return null;

        return Regex.Replace(name, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}
