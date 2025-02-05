namespace Domain.Customer;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(CustomerId id); // Metodo

    Task Add(Customer customer); // Metodo
}