using System.Collections.Generic;

namespace Intercom.Customer.API.Tests
{
    public class CustomerFactory
    {
        public static List<Contract.CustomerInfo> GetCustomerInfos()
            => new List<Contract.CustomerInfo>()
            {
                new Contract.CustomerInfo { UserId = 1, Name = "fName1 lname1", Latitude = 1, Longitude = 1},
                new Contract.CustomerInfo { UserId = 2, Name = "fName2 lname2", Latitude = 1, Longitude = 1},
                new Contract.CustomerInfo { UserId = 3, Name = "fName3 lname3", Latitude = 2, Longitude = 2},
                new Contract.CustomerInfo { UserId = 4, Name = "fName4 lname4", Latitude = 2, Longitude = 2}
            };

        public static List<Contract.Customer> GetCustomers()
            => new List<Contract.Customer>()
            {
                new Contract.Customer { UserId = 1, Name = "fName1 lname1" },
                new Contract.Customer { UserId = 2, Name = "fName2 lname2" },
                new Contract.Customer { UserId = 3, Name = "fName3 lname3" },
                new Contract.Customer { UserId = 4, Name = "fName4 lname4" }
            };
    }
}
