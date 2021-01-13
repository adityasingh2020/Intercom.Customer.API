using Intercom.Customer.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intercom.Customer.Domain
{
    public class CustomerService : ICustomerService
    {
        readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<List<Contract.Customer>> GetNearByCustomers(double distance, string filepath)
            => _customerRepository.GetNearByCustomers(distance, filepath);
    }
}
