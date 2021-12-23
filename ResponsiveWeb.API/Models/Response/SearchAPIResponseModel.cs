using Newtonsoft.Json;
using System.Collections.Generic;

namespace ResponsiveWeb.API.Models.Response
{
    public class SearchAPIResponseModel
    {
        [JsonProperty("errorMessage")]
        public object ErrorMessage { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("firstRecord")]
        public long FirstRecord { get; set; }

        [JsonProperty("lastRecord")]
        public long LastRecord { get; set; }

        [JsonProperty("totalPages")]
        public long TotalPages { get; set; }

        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty("lastPage")]
        public object LastPage { get; set; }

        [JsonProperty("values")]
        public List<TemperaturesValue> Values { get; set; }

        [JsonProperty("facets")]
        public List<Facet> Facets { get; set; }

        [JsonProperty("durationMilliseconds")]
        public double DurationMilliseconds { get; set; }


        // ===
        public class Facet
        {
            [JsonProperty("fieldName")]
            public string FieldName { get; set; }

            [JsonProperty("resultsCount")]
            public long ResultsCount { get; set; }

            [JsonProperty("values")]
            public List<FacetValue> Values { get; set; }
        }

        // ===
        public partial class FacetValue
        {
            [JsonProperty("count")]
            public long Count { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("selected")]
            public bool Selected { get; set; }
        }

        // ===

        public partial class TemperaturesValue
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("abstract")]
            public string Abstract { get; set; }

            [JsonProperty("date")]
            public string Date { get; set; }

            [JsonProperty("dateLabel")]
            public string DateLabel { get; set; }

            [JsonProperty("displayCommunity")]
            public object DisplayCommunity { get; set; }

            [JsonProperty("displayDocumentType")]
            public string DisplayDocumentType { get; set; }

            [JsonProperty("displayMode")]
            public object DisplayMode { get; set; }

            [JsonProperty("displayProjectStatus")]
            public object DisplayProjectStatus { get; set; }

            [JsonProperty("displayProjectType")]
            public object DisplayProjectType { get; set; }

            [JsonProperty("downloadUrl")]
            public string DownloadUrl { get; set; }

            [JsonProperty("resourceSize")]
            public string ResourceSize { get; set; }

            [JsonProperty("itemType")]
            public string ItemType { get; set; }

            [JsonProperty("thumbnail")]
            public object Thumbnail { get; set; }

            [JsonProperty("thumbnailAltText")]
            public object ThumbnailAltText { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }
        }
    }
}
