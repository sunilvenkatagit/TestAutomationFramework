using Newtonsoft.Json;

namespace ResponsiveWeb.API.Models.Response
{
    public class DeliveryOptionsModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("emailEnabled")]
        public bool EmailEnabled { get; set; }

        [JsonProperty("phone")]
        public object Phone { get; set; }

        [JsonProperty("smsEnabled")]
        public bool SmsEnabled { get; set; }

        [JsonProperty("smsValidationPending")]
        public bool SmsValidationPending { get; set; }
    }
}
