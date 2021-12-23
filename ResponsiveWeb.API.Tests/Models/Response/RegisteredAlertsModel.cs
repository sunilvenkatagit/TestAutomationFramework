using Newtonsoft.Json;

namespace ResponsiveWeb.API.Models.Response
{
    public class RegisteredAlertsModel
    {
        [JsonProperty("routeNumber")]
        public string RouteNumber { get; set; }
        [JsonProperty("activeDaysOfWeek")]
        public int ActiveDaysOfWeek { get; set; }
        [JsonProperty("isEnabled")]
        public bool IsEnabled { get; set; }
    }
}
