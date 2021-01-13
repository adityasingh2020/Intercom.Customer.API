using Intercom.Customer.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intercom.Customer.Infrastructure
{
    public interface ICustomerRepository
    {
        Task<List<Contract.Customer>> GetNearByCustomers(double distance, string filepath);
    }
}
