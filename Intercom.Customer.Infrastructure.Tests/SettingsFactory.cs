using Intercom.Customer.Contract;

namespace Intercom.Customer.Infrastructure.Tests
{
    public class SettingsFactory
    {
        public static AppSettings GetAppSettings()
            => new AppSettings
            {
                CustomerRecordFilePath = "https://test.url.com",
                Distance = 100,
                IntercomCoords = new Location { Latitude = 53.339428, Longitude = -6.257664 }
            };
                        
    }
}
