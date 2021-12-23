using Newtonsoft.Json;

namespace ResponsiveWeb.API.Models.Response
{
    public class GTFSRoutesModel
    {
        [JsonProperty("routeNumber")]
        public string RouteNumber { get; set; }

        [JsonProperty("routeDescription")]
        public string RouteDescription { get; set; }

        [JsonProperty("routeType")]
        public int RouteType { get; set; }
    }
}
