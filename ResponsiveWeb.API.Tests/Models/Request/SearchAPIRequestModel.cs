using Newtonsoft.Json;
using System.Collections.Generic;

namespace ResponsiveWeb.API.Models.Request
{
    public class SearchAPIRequestModel
    {
        // Constructor
        public SearchAPIRequestModel()
        {
            TemplateIds = new List<string>()
                                                {
                                                    "{1be531db-308b-45ae-b8ed-4c8ade9f3aaa}",
                                                    "{ac5fc619-1041-463b-878f-bc7604cebf6d}",
                                                    "{79691c0e-5699-4e03-ba42-71f1477ac27f}",
                                                    "{f7c0bdbc-35eb-4f7e-b89d-0733d6cc8de9}",
                                                    "{21197BC3-885C-4AAC-94FB-0548D408617B}"
                                                };
            PathIds = new List<string>()
                                                {
                                                    "{8e26d03f-369d-4346-825c-f7a16af50993}",
                                                    "{ee4e2425-8420-4676-8134-f2af005fb1eb}",
                                                    "{03290340-65ae-418e-964b-cb819a8d6932}",
                                                    "{E3EC5185-B943-4BE6-93BC-7388BE3C5593}"
                                                };

            ExcludePathIDs = new List<string>()
                                                {
                                                    "{1cafe3cd-ad36-4f72-9f4d-1fb243af0f73}"
                                                };

            First = 0;

            Facets = new List<Facet>()
                                    {
                                        {
                                            new Facet()
                                                    {
                                                        FieldName = "computedcontenttypefield",
                                                        SortOrder = "asc",
                                                        SelectedValues = new List<object>(),
                                                        FieldType = "String"
                                                    }
                                        },
                                        {
                                            new Facet()
                                                    {
                                                        FieldName = "computeditemdateyearfield",
                                                        SortOrder = "desc",
                                                        SelectedValues = new List<object>(),
                                                        FieldType = "String"
                                                    }
                                        },
                                        {
                                            new Facet()
                                                    {
                                                        FieldName = "computedmodefield",
                                                        SortOrder = "asc",
                                                        SelectedValues = new List<object>(),
                                                        FieldType = "Collection"
                                                    }
                                        },
                                        {
                                            new Facet()
                                                    {
                                                        FieldName = "computedmediatypefield",
                                                        SortOrder = "asc",
                                                        SelectedValues = new List<object>(),
                                                        FieldType = "Collection"
                                                    }
                                        }
                                    };

            DisplayCount = 10;

            OrderBy = "Relevance";

            Tags = new List<object>();

            IndexName = "sitecore_public_index";
        }

        // =====
        [JsonProperty("TemplateIds")]
        public List<string> TemplateIds { get; set; }

        [JsonProperty("PathIds")]
        public List<string> PathIds { get; set; }

        [JsonProperty("ExcludePathIDs")]
        public List<string> ExcludePathIDs { get; set; }

        [JsonProperty("First")]
        public long First { get; set; }

        [JsonProperty("Facets")]
        public List<Facet> Facets { get; set; }

        [JsonProperty("DisplayCount")]
        public long DisplayCount { get; set; }

        [JsonProperty("OrderBy")]
        public string OrderBy { get; set; }

        [JsonProperty("SearchQuery")]
        public string SearchQuery { get; set; }

        [JsonProperty("Tags")]
        public List<object> Tags { get; set; }

        [JsonProperty("IndexName")]
        public string IndexName { get; set; }


        public partial class Facet
        {
            // Constructor
            public Facet()
            {
                DisplayCount = 100;
                UsesAndOperator = true;
            }

            // ====
            [JsonProperty("FieldName")]
            public string FieldName { get; set; }

            [JsonProperty("SortOrder")]
            public string SortOrder { get; set; }

            [JsonProperty("DisplayCount")]
            public long DisplayCount { get; set; }

            [JsonProperty("SelectedValues")]
            public List<object> SelectedValues { get; set; }

            [JsonProperty("FieldType")]
            public string FieldType { get; set; }

            [JsonProperty("UsesAndOperator")]
            public bool UsesAndOperator { get; set; }
        }

    }
}
