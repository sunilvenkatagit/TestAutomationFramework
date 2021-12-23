using Newtonsoft.Json;

namespace ResponsiveWeb.API.Models.Response
{
    public class GtfsLastRefreshDateModel
    {
        [JsonProperty("refreshDateTime")]
        public string RefreshDateTime { get; set; }
    }
}
