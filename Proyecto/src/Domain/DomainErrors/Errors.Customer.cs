using ErrorOr;

namespace Domain.DomainErrors;

// 
public static partial class Errors
{
    
    public static class Customer // 
    {
        // 
        public static Error PhoneNumberWithBadFormat =>
                Error.Validation("Customer.PhoneNumber", "Phone number is required. Format valid [9 digits]");

        public static Error AddressIsRequired =>
                Error.Validation("Customer.Address", "Customer address is required.");

        public static Error CustomerIsNull =>
                Error.Validation("Customer.Customer", "Customer is null"); 
    }
}