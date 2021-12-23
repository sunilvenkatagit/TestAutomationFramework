using Newtonsoft.Json;

namespace ResponsiveWeb.API.Models.Response
{
    public class SiteNavErrorModel
    {
        [JsonProperty("Error")]
        public string Error { get; set; }
    }
}
