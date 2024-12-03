using System.Text.RegularExpressions;

namespace Catalog.API.Utils;

/// <summary>
/// The purpose is to export to the documentation (openapi.json) name of URLs and paraters as "my-sample-endpoint"
/// and not like 'MySampleEndpoint'
/// </summary>
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
