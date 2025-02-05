using Domain.Customer;
using Domain.ObjetosValor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    // Clase para configurar la entidad Customer
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        // Configuración de la entidad Customer
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            // builder.ToTable("Customers"); // Configuración de la tabla Customers

            builder.HasKey(c => c.Id); // Primary Key
            // Configuración de la propiedad Id
            builder.Property(c => c.Id).HasConversion(
                id => id.Value,
                value => new CustomerId(value) // Value Object
            );

            builder.Property(c => c.Name).IsRequired().HasMaxLength(50); // Configuración de la propiedad Name

            builder.Property(c => c.LastName).HasMaxLength(50); // Configuración de la propiedad LastName

            builder.Ignore(c => c.FullName); // Ignorar propiedad FullName

            builder.Property(c => c.Email).HasMaxLength(255); // Configuración de la propiedad Email

            builder.HasIndex(c => c.Email).IsUnique(); // Configuración de índice para la propiedad Email

            builder.Property(c => c.PhoneNumber).HasConversion(
                phone => phone.Value,
                value => PhoneNumber.Create(value)! // Value Object
            ).HasMaxLength(9); // Configuración de la propiedad Phone

            builder.OwnsOne(c => c.Address, a =>
            {
                a.Property(a => a.Street).HasMaxLength(100); // Configuración de la propiedad Street
                a.Property(a => a.City).HasMaxLength(50); // Configuración de la propiedad City
                a.Property(a => a.State).HasMaxLength(50); // Configuración de la propiedad State
                a.Property(a => a.ZipCode).HasMaxLength(10).IsRequired(); // Configuración de la propiedad ZipCode
            });

        }
    }
}