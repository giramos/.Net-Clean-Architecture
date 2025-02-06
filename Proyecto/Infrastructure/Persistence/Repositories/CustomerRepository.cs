using Domain.Customer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    // interfaz para el repositorio de clientes
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context; // propiedad de solo lectura para acceder al contexto de la aplicación

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); // asignar el contexto a la propiedad
        }

        public async Task<Customer?> GetByIdAsync(CustomerId id) => await _context.Customers.SingleOrDefaultAsync(c => c.Id == id); // obtener un cliente por su identificador

        public async Task Add(Customer customer) => await _context.Customers.AddAsync(customer); // agregar un cliente al contexto

        public void Delete(Customer customer) => _context.Customers.Remove(customer); // eliminar un cliente del contexto

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}