using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intercom.Customer.Domain
{
    public interface ICustomerService
    {
        Task<List<Contract.Customer>> GetNearByCustomers(double distance, string filepath);
    }
}
