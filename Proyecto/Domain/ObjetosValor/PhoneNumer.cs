using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace Domain.ObjetosValor;

/// <summary>
/// Numero de telefono
/// </summary>
public partial record PhoneNumber
{
    private const int DefaultLength = 9; // 9 digitos
    private const string Pattern = @"^\d{9}$"; // 9 digitos
    private PhoneNumber(string value) => Value = value;

    public string Value { get; init; }

    /// <summary>
    /// Crea un objeto PhoneNumber si el valor es valido
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static PhoneNumber? Create(string value)
    {
        if (string.IsNullOrEmpty(value) || !PhoneNumberRegex().IsMatch(value) || value.Length != DefaultLength)
        {
            return null;
        }
        return new PhoneNumber(value);
    }
    /// <summary>
    /// Expresion regular para validar el valor
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Pattern)]
    private static partial Regex PhoneNumberRegex();
}