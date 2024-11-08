using System.Reflection;
using System.Text;

namespace Catalog.Business.Utils;
public static class CsvReader
{
    const char TextQualifier = '"';

    public static List<T> ReadFromCsv<T>(MemoryStream csv, int rowsNumberToRead = int.MaxValue, char columnsDelimiter = ',') where T : new()
    {
        var results = new List<T>();

        // Validation
        if (csv == null || csv.Length <= 3) // 3 - the least amount of characters required for potentially valid csv file
        {
            throw new ArgumentException("Invalid csv file");
        }

        var streamReader = new StreamReader(csv, Encoding.UTF8);

        try
        {

            // Build model
            var headers = streamReader.ReadLine() ?? "";
            var fieldsNameNormalized = ExtractColumns(headers, columnsDelimiter)
                .Select(x => string.IsNullOrWhiteSpace(x) ? string.Empty : x.ToLower())
                .ToList();

            // parse row
            while (rowsNumberToRead-- > 0 && !streamReader.EndOfStream)
            {
                var values = ExtractColumns(streamReader.ReadLine() ?? "", columnsDelimiter);

                var newObject = BuildObject<T>(fieldsNameNormalized, values);
                results.Add(newObject);
            }
        }
        catch (Exception)
        {
            // log
            return default!;
        }

        return results;
    }

    private static T BuildObject<T>(List<string> headers, List<string> values) where T : new()
    {
        var isntanceType = typeof(T);
        var instance = new T();

        var properties = isntanceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        for (int i = 0; i < headers.Count; i++)
        {
            var prop = properties.FirstOrDefault(x => x.Name.Equals(headers[i], StringComparison.InvariantCultureIgnoreCase));
            if (prop != null
                && !string.IsNullOrWhiteSpace(values.ElementAtOrDefault(i))
                && TryConvert(values[i], prop.PropertyType, out object convertedValue))
            {
                prop.SetValue(instance, convertedValue);
            }
        }


        return instance;
    }

    private static bool TryConvert(string value, Type type, out object convertedValue)
    {
        try
        {
            Type targetType = type;

            if (targetType == typeof(string))
            {
                convertedValue = value;
                return true;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                convertedValue = value;
                return false;
            }

            // Nullable
            var nullableType = targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>);
            if (nullableType)
            {
                var underlyingNullableType = Nullable.GetUnderlyingType(targetType);
                if (underlyingNullableType == default)
                {
                    convertedValue = value;
                    return false;
                }

                targetType = underlyingNullableType;
            }

            // Try use TryParse if available
            Type[] argTypes = [typeof(string), targetType.MakeByRefType()];
            var tryParseMethod = targetType.GetMethod("TryParse", argTypes); // for most of the default types
            if (tryParseMethod != null)
            {
                object[] args = [value, null!];
                var parseResult = (bool)tryParseMethod.Invoke(null, args)!;
                if (parseResult) // successfull parse
                {
                    convertedValue = args[1];
                    return true;
                }
            }

            // Handle enums (number to enum value)
            if (type.IsEnum)
            {
                try
                {

                    if (int.TryParse(value, out int res))
                    {
                        var enumObject = Enum.ToObject(type, res);
                        convertedValue = enumObject;
                        return true;
                    }
                }
                catch
                {
                    convertedValue = default!;
                    return false;
                }
            }

            // Try use Convertor (IConvertable IF)
            var convesionResult = Convert.ChangeType(value, targetType);
            if (convesionResult != null)
            {
                convertedValue = convesionResult;
                return true;
            }

            convertedValue = default!;
            return false;
        }
        catch
        {
            convertedValue = default!;
            return false;
        }
    }

    private static List<string> ExtractColumns(string row, char delimiter)
    {
        var columns = new List<string>();

        bool isOpenTextQualifier = false;
        int positionOfLastDelimiter = -1;

        var rowNormalized = row.Trim();//.Replace("\\\"", "\"");

        for (int i = 0; i < rowNormalized.Length; i++)
        {
            if (rowNormalized[i] == delimiter && !isOpenTextQualifier)
            {
                if ((i - positionOfLastDelimiter) > 1)
                {
                    columns.Add(rowNormalized[(positionOfLastDelimiter + 1)..i].Trim('"').Replace("\"\"", "\"")); // extra code for JSON
                }
                else
                {
                    columns.Add(null!);
                }

                positionOfLastDelimiter = i;
                continue;
            }


            if (rowNormalized[i] == TextQualifier)
            {
                isOpenTextQualifier = !isOpenTextQualifier;
            }
        }


        if ((rowNormalized.Length - positionOfLastDelimiter) > 1)
        {
            columns.Add(rowNormalized[(positionOfLastDelimiter + 1)..]);
        }

        return columns;
    }

}
