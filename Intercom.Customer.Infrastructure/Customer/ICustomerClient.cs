using Intercom.Customer.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intercom.Customer.Infrastructure
{
    public interface ICustomerClient
    {
        Task<List<CustomerInfo>> GetCustomerDeatils(string customerRecordUrl);
    }
}
