using Catalog.Common.Result;
using System.Reflection;
using System.Text;

namespace Catalog.Common.Utils;
public static class CsvReader
{
    /// <summary>
    /// It is a character that denotes string that will be considered as a single column, not parsed into multiple if there are 
    /// any column delimiters within it
    /// </summary>
    public const char TextQualifier = '"';

    /// <summary>
    /// Parse a csv file passed as memory stream into a list of objects of type <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    ///     Value of a text qualifier is stored in <see cref="TextQualifier"/>
    /// </para>
    /// 
    /// <para>
    ///     Method will throw an inner exception if it occurs
    /// </para>
    /// </remarks>
    /// <param name="csv">Csv file as memory stream. Header row is mandatory!</param>
    /// <param name="rowsNumberToRead">Number of rows to read</param>
    /// <param name="columnsDelimiter">Character that denotes a column delimiter</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Result<List<T>> ReadFromCsv<T>(MemoryStream csv, int rowsNumberToRead = int.MaxValue, char columnsDelimiter = ',') where T : new()
    {
        var results = new List<T>();

        var validationResult = ValidateInputCsv(csv);
        if (validationResult.IsFailure)
        {
            return validationResult.Error switch
            {
                Error => validationResult.Error,
                _ => Errors.ServerInternalError
            };
        }

        using var streamReader = new StreamReader(csv, Encoding.UTF8);

        // Extract list of columns
        var headers = streamReader.ReadLine() ?? "";
        var fieldsNameNormalized = ExtractColumns(headers, columnsDelimiter)
            .Select(x => string.IsNullOrWhiteSpace(x) ? string.Empty : x.ToLower())
            .ToList();

        // Parse rows
        while (rowsNumberToRead-- > 0 && !streamReader.EndOfStream)
        {
            var values = ExtractColumns(streamReader.ReadLine() ?? "", columnsDelimiter);

            var newObject = BuildObject<T>(fieldsNameNormalized, values);
            results.Add(newObject);
        }

        return results;
    }

    private static ResultVoid ValidateInputCsv(MemoryStream csv)
    {
        if (csv == null || csv.Length <= 3) // 3 - the least amount of characters required for potentially valid csv file
        {
            return ResultVoid.FailureVoid(Errors.RequestGenericError("The provided csv file is null, empty or invalid"));
        }

        return ResultVoid.SuccessVoid();
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

            var resultsForTypesWithoutConversion = HandleTypesThatDoNotNeedConversion(value, targetType);
            if (resultsForTypesWithoutConversion.isSuccessfull)
            {
                convertedValue = resultsForTypesWithoutConversion.convertedValue!;
                return true;
            }

            targetType = TryExtractUnderlyingTypeFromNullableTypes(targetType);


            var resultsForTryParseMethod = TryConvertUsingTryParseMethod(value, targetType);
            if (resultsForTryParseMethod.isSuccessfull)
            {
                convertedValue = resultsForTryParseMethod.convertedValue!;
                return true;
            }


            var resultsForEnumTypeConversion = TryConvertEnumTypes(value, type);
            if (resultsForEnumTypeConversion.isSuccessfull)
            {
                convertedValue = resultsForEnumTypeConversion.convertedValue!;
                return true;
            }

            // Try use Convertor (IConvertable IF)
            var resultsForTypesImplementingIConvertable = TryConvertUsingIConvertableInterface(value, targetType);
            if (resultsForTypesImplementingIConvertable.isSuccessfull)
            {
                convertedValue = resultsForTypesImplementingIConvertable.convertedValue!;
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

    private static (bool isSuccessfull, object? convertedValue) TryConvertUsingIConvertableInterface(string value, Type targetType)
    {
        try
        {
            var convesionResult = Convert.ChangeType(value, targetType);
            if (convesionResult != null)
            {
                return (true, convesionResult);
            }
        }
        catch
        {
            return (false, default);
        }

        return (false, default);
    }

    private static (bool isSuccessfull, object? convertedValue) TryConvertEnumTypes(string value, Type type)
    {
        if (!type.IsEnum)
        {
            return (false, default);
        }

        try
        {
            if (int.TryParse(value, out int res))
            {
                var enumObject = Enum.ToObject(type, res);
                return (true, enumObject);
            }
        }
        catch
        {
            return (false, default);
        }

        return (false, default);
    }

    private static (bool isSuccessfull, object? convertedValue) TryConvertUsingTryParseMethod(string value, Type targetType)
    {
        Type[] argTypes = [typeof(string), targetType.MakeByRefType()];
        var tryParseMethod = targetType.GetMethod("TryParse", argTypes); // for most of the default types

        if (tryParseMethod != null)
        {
            object[] args = [value, null!];
            var parseResult = (bool)tryParseMethod.Invoke(null, args)!;
            if (parseResult)
            {
                return (true, args[1]);
            }
        }

        return (false, default);
    }

    private static Type TryExtractUnderlyingTypeFromNullableTypes(Type targetType)
    {
        var nullableType = targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>);
        if (nullableType)
        {
            var underlyingNullableType = Nullable.GetUnderlyingType(targetType);
            if (underlyingNullableType != default)
            {
                return underlyingNullableType;
            }
        }

        return targetType;
    }

    private static (bool isSuccessfull, object? convertedValue) HandleTypesThatDoNotNeedConversion(string value, Type targetType)
    {
        if (targetType == typeof(string))
        {
            return (true, value);
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            return (true, value);
        }

        return (false, default);
    }

    private static List<string> ExtractColumns(string row, char delimiter)
    {
        var columns = new List<string>();

        bool isOpenTextQualifier = false;
        int positionOfLastDelimiter = -1;

        var rowNormalized = row.Trim();

        for (int i = 0; i < rowNormalized.Length; i++)
        {
            if (rowNormalized[i] == delimiter && !isOpenTextQualifier)
            {
                if ((i - positionOfLastDelimiter) > 1)
                {
                    columns.Add(rowNormalized[(positionOfLastDelimiter + 1)..i].Trim('"').Replace("\"\"", "\"")); // replace to handle JSONs inside columns
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
