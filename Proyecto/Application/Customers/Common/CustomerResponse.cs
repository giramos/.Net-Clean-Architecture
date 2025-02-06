namespace Customers.Common;

//  Clase sellada que recibe un Guid, un string, un string, un string y un AddressResponse
public record CustomerResponse(Guid Id, string Name, string Email, string Phone, AddressResponse Address);

// Clase sellada que recibe un string, un string, un string, un string y un string
public record AddressResponse(string Street, string City, string State, string Country, string ZipCode);