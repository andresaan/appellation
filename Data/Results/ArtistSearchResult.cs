using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Data.Results
{

    public class ArtistSearchResult
    {
        [JsonProperty(PropertyName = "artists")]
        public ArtistSearchSummary Summary { get; set; }
    }

    public class ArtistSearchSummary
    {
        public string Href { get; set; } = string.Empty;

        [JsonProperty (PropertyName = "items")]
        public Artist[] Artists { get; set; } = new Artist[0];
        public int Limit { get; set; }
        public string Next { get; set; } = string.Empty;
        public int Offset { get; set; }
        public object Previous { get; set; } = string.Empty;
        public int Total { get; set; }
    }

    public class Artist : ArtistBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; } // keep
        public Image[] Images { get; set; }
        public string Uri { get; set; }
        public string Href { get; set; }
        public Followers Followers { get; set; }
        public string[] Genres { get; set; } // keep
        [JsonProperty("external_urls")]
        public External_Urls ExternalUrls { get; set; }
        public string Type { get; set; }
        
    }

}

