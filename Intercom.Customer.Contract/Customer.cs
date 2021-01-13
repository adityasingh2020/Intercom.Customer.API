using Newtonsoft.Json;

namespace Intercom.Customer.Contract
{
    public class Customer
    {
        [JsonProperty(propertyName:"user_id", Required = Required.Always)]
        public int UserId { get; set; }

        [JsonProperty(propertyName: "name", Required = Required.Always)]
        public string Name { get; set; }
    }
}
