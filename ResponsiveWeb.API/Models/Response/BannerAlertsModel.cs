using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ResponsiveWeb.API.Models.Response
{
    public class BannerAlertsModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("informedEntities")]
        public List<InformedEntity> InformedEntities { get; set; }

        [JsonProperty("alertLifecycle")]
        public object AlertLifecycle { get; set; }

        [JsonProperty("certainty")]
        public string Certainty { get; set; }

        [JsonProperty("effect")]
        public string Effect { get; set; }

        [JsonProperty("startTime")]
        public DateTimeOffset StartTime { get; set; }

        [JsonProperty("endTime")]
        public DateTimeOffset? EndTime { get; set; }

        [JsonProperty("bannerText")]
        public string BannerText { get; set; }

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


        public class InformedEntity
        {
            [JsonProperty("mode")]
            public string Mode { get; set; }

            [JsonProperty("lines")]
            public List<string> Lines { get; set; }
        }

    }
}
