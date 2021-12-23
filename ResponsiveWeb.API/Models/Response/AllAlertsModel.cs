using Newtonsoft.Json;
using System;

namespace ResponsiveWeb.API.Models.Response
{
    public class AllAlertsModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("group")]
        public long Group { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("critical")]
        public bool Critical { get; set; }

        [JsonProperty("advisory")]
        public bool Advisory { get; set; }

        [JsonProperty("mode")]
        public long Mode { get; set; }

        [JsonProperty("routeId")]
        public string RouteId { get; set; }

        [JsonProperty("routeLongName")]
        public string RouteLongName { get; set; }

        [JsonProperty("stationId")]
        public string StationId { get; set; }

        [JsonProperty("stationName")]
        public string StationName { get; set; }

        [JsonProperty("alertLifecycle")]
        public string AlertLifecycle { get; set; }

        [JsonProperty("certainty")]
        public string Certainty { get; set; }

        [JsonProperty("effect")]
        public string Effect { get; set; }

        [JsonProperty("endStamp")]
        public long EndStamp { get; set; }

        [JsonProperty("startTime")]
        public DateTimeOffset StartTime { get; set; }

        [JsonProperty("endTime")]
        public DateTimeOffset? EndTime { get; set; }

        [JsonProperty("alertText")]
        public string AlertText { get; set; }

        [JsonProperty("header")]
        public string Header { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("lastModified")]
        public DateTimeOffset LastModified { get; set; }
    }
}
