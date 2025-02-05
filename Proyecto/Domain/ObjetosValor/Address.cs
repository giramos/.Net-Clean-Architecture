namespace Domain.ObjetosValor;

/// <summary>
/// Direccion
/// </summary>
public partial record Address{
    private Address(string street, string city, string state, string country, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string Country { get; init; }
    public string ZipCode { get; init; }

    public static Address? Create(string street, string city, string state, string country, string zipCode)
    {
        if (string.IsNullOrEmpty(street) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(country) || string.IsNullOrEmpty(zipCode))
        {
            return null;
        }
        return new Address(street, city, state, country, zipCode);
    }
}