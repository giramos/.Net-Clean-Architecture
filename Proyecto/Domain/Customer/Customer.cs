using Domain.ObjetosValor;
using Domain.Primitivos;

namespace Domain.Customer;

// Customer es una entidad, por lo que hereda de AggregateRoot
public sealed class Customer : AggregateRoot
{
    public Customer(CustomerId id, string name, string lastName, string email, PhoneNumber phoneNumber, Address address)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }
    public Customer()
    {
    }

    public CustomerId Id { get; private set; } // Value Object
    public string Name { get; private set; } = string.Empty; // Propiedad
    public string LastName { get; set; } = string.Empty; // Propiedad

    public string FullName => $"{Name} {LastName}"; // Propiedad de solo lectura
    public string Email { get; private set; } = string.Empty; // Propiedad

    public PhoneNumber PhoneNumber { get; private set; } // Value Object

    public Address Address { get; private set; } // Value Object

    public bool IsActive { get;  set; } // Propiedad
}