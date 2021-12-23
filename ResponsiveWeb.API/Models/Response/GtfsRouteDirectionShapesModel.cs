using Newtonsoft.Json;
using System.Collections.Generic;

namespace ResponsiveWeb.API.Models.Response
{
    public class GtfsRouteDirectionShapesModel
    {
        [JsonProperty("s")]
        public List<ShapePoints> S { get; set; }
    }
    public partial class ShapePoints
    {
        [JsonProperty("sla")]
        public double sla { get; set; }

        [JsonProperty("sln")]
        public double sln { get; set; }

    }
}




