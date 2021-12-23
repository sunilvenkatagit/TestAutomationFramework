using Newtonsoft.Json;
using System.Collections.Generic;

namespace ResponsiveWeb.API.Models.Response.GTFS_API
{
    public class GtfsStopOrStationScheduleModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("sc")]
        public long Sc { get; set; }

        [JsonProperty("sn")]
        public string Sn { get; set; }

        [JsonProperty("la")]
        public double La { get; set; }

        [JsonProperty("lo")]
        public double Lo { get; set; }

        [JsonProperty("lt")]
        public long Lt { get; set; }

        [JsonProperty("wc")]
        public long Wc { get; set; }

        [JsonProperty("sd")]
        public string Sd { get; set; }

        [JsonProperty("r")]
        public List<Routes> R { get; set; }
    }

    public partial class Routes
    {
        [JsonProperty("rs")]
        public string Rs { get; set; }

        [JsonProperty("dn")]
        public string Dn { get; set; }

        [JsonProperty("rt")]
        public long Rt { get; set; }

        [JsonProperty("t")]
        public List<Trips> T { get; set; }
    }

    public partial class Trips
    {
        [JsonProperty("ti")]
        public string Ti { get; set; }

        [JsonProperty("th")]
        public string Th { get; set; }

        [JsonProperty("dtm")]
        public string Dtm { get; set; }

        [JsonProperty("dt")]
        public string Dt { get; set; }

        [JsonProperty("ut")]
        public long Ut { get; set; }

        [JsonProperty("po")]
        public bool Po { get; set; }

        [JsonProperty("do")]
        public bool Do { get; set; }
    }
}
