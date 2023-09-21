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

    public class ArtistComplex : ArtistBase // contains all artist information spotify can provide through an endpoint
    {
        public int Popularity { get; set; } 
        public Image[] Images { get; set; } = Array.Empty<Image>();
        public Followers Followers { get; set; } = new Followers();
        public string[] Genres { get; set; } = Array.Empty<string>();

    }

    public class ArtistSimple : ArtistBase // spotifies simple artist obj
    {
    }

}
