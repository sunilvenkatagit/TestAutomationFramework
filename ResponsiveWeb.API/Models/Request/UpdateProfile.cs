using Newtonsoft.Json;

namespace ResponsiveWeb.API.Models.Request
{
    public class UpdateProfile
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
