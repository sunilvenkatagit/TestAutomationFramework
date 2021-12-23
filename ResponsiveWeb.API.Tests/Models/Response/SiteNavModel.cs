using System.Collections.Generic;
using Newtonsoft.Json;

namespace ResponsiveWeb.API.Models.Response
{
    public class SiteNavModel
    {
        [JsonProperty("MenuItemLinkUrl")]
        public string MenuItemLinkUrl { get; set; }

        [JsonProperty("MenuItemName")]
        public string MenuItemName { get; set; }

        [JsonProperty("MenuItemTabTarget")]
        public bool MenuItemTabTarget { get; set; }

        [JsonProperty("MenuItemTabImageUrl")]
        public string MenuItemTabImageUrl { get; set; }

        [JsonProperty("MenuItemTabImageHeight")]
        public string MenuItemTabImageHeight { get; set; }

        [JsonProperty("MenuItemTabImageWidth")]
        public string MenuItemTabImageWidth { get; set; }

        [JsonProperty("MenuItemTabImageAlt")]
        public string MenuItemTabImageAlt { get; set; }

        [JsonProperty("MenuItemTabAriaLabel")]
        public string MenuItemTabAriaLabel { get; set; }

        [JsonProperty("Children")]
        public List<SiteNavModel> Children { get; set; }
    }
}
