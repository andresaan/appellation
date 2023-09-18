using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Results
{
    public abstract class ArtistBase
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        [JsonProperty("external_urls")]
        public External_Urls ExternalUrls { get; set; } = new External_Urls();
        public string Href { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
    }



}
