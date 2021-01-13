using System.Collections.Generic;

namespace Intercom.Customer.Infrastructure.Tests
{
    public class CustomerFactory
    {
        public static List<Contract.CustomerInfo> GetCustomerInfos()
            => new List<Contract.CustomerInfo>()
            {
                new Contract.CustomerInfo { UserId = 1, Name = "fName1 lname1", Latitude = 53.34, Longitude = -6.3877505}, // < 10 KM 
                new Contract.CustomerInfo { UserId = 2, Name = "fName2 lname2", Latitude = 53.44, Longitude = -6.24},      // < 20 KM 
                new Contract.CustomerInfo { UserId = 3, Name = "fName3 lname3", Latitude = 53.54, Longitude = -6.25},      // < 30 KM 
                new Contract.CustomerInfo { UserId = 4, Name = "fName4 lname4", Latitude = 53.64, Longitude = -6.22},      // < 40 KM 
                new Contract.CustomerInfo { UserId = 4, Name = "fName4 lname4", Latitude = 54, Longitude = -6.22},         // < 100 KM 
                new Contract.CustomerInfo { UserId = 4, Name = "fName4 lname4", Latitude = 55, Longitude = -6.22},         // < 200 KM 
                new Contract.CustomerInfo { UserId = 4, Name = "fName4 lname4", Latitude = 56, Longitude = -6.22}          // < 300 KM 
            };


       
    }
}
