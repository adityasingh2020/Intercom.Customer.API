using Intercom.Customer.Contract;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intercom.Customer.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        readonly ICustomerClient _customerClient;
        readonly AppSettings _settings;

        public CustomerRepository(IOptions<AppSettings> settings, ICustomerClient customerClient)
        {
            _settings = settings.Value;
            _customerClient = customerClient;
        }

        public async Task<List<Contract.Customer>> GetNearByCustomers(double distance, string filepath)
        {
            var path = string.IsNullOrWhiteSpace(filepath) ? _settings.CustomerRecordFilePath : filepath;
            var customerInfos = await _customerClient.GetCustomerDeatils(path);

            return customerInfos
               .Where(c => GetDistance(new Location(c.Latitude, c.Longitude)) <= distance)
               .Select(c => new Contract.Customer { UserId = c.UserId, Name = c.Name })
               .OrderBy(c => c.UserId).ToList();
        }

        private double GetDistance(Location custLoc)
            => Haversine.Calculate(_settings.IntercomCoords.Latitude, _settings.IntercomCoords.Longitude, custLoc.Latitude, custLoc.Longitude);
    }
}
