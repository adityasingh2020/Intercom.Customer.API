using Newtonsoft.Json;

namespace Intercom.Customer.Contract
{
    public class CustomerInfo : Customer
    {
        [JsonProperty(propertyName: "latitude", Required = Required.Always)]
        public double Latitude { get; set; }

        [JsonProperty(propertyName: "longitude", Required = Required.Always)]
        public double Longitude { get; set; }
    }
}
