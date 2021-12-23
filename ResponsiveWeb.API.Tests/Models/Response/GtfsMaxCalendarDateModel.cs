using Newtonsoft.Json;

namespace ResponsiveWeb.API.Models.Response
{
    public class GtfsMaxCalendarDateModel
    {
        [JsonProperty("maxDate")]
        public string MaxDate { get; set; }
    }
}
