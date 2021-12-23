using Newtonsoft.Json;
using System.Collections.Generic;

namespace ResponsiveWeb.API.Models.Response
{
    public class GtfsRouteScheduleModel
    {
        [JsonProperty("rs")]
        public string Rs { get; set; }

        [JsonProperty("rl")]
        public string Rl { get; set; }

        [JsonProperty("rt")]
        public long Rt { get; set; }

        [JsonProperty("d")]
        public List<Directions> D { get; set; }

        [JsonProperty("sd")]
        public string Sd { get; set; }

        [JsonProperty("s")]
        public List<Stops> S { get; set; }
    }

    public partial class Directions
    {
        [JsonProperty("di")]
        public long Di { get; set; }

        [JsonProperty("cd")]
        public string Cd { get; set; }

        [JsonProperty("dn")]
        public string Dn { get; set; }
    }

    public partial class Stops
    {
        [JsonProperty("sc")]
        public long Sc { get; set; }

        [JsonProperty("sn")]
        public string Sn { get; set; }

        [JsonProperty("la")]
        public double La { get; set; }

        [JsonProperty("lo")]
        public double Lo { get; set; }

        [JsonProperty("wc")]
        public object Wc { get; set; }

        [JsonProperty("ss")]
        public long Ss { get; set; }

        [JsonProperty("st")]
        public List<StopTimes> St { get; set; }
    }

    public partial class StopTimes
    {
        [JsonProperty("ts")]
        public long Ts { get; set; }

        [JsonProperty("dt")]
        public string Dt { get; set; }

        [JsonProperty("po")]
        public bool Po { get; set; }

        [JsonProperty("do")]
        public bool Do { get; set; }
    }
}
