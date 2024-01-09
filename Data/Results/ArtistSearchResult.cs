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
        public ArtistSearchSummary Summary { get; set; } = new ArtistSearchSummary();
    }

    public class ArtistSearchSummary
    {
        //public string Href { get; set; } = string.Empty;

        [JsonProperty (PropertyName = "items")]
        public ArtistComplex[] Artists { get; set; } = Array.Empty<ArtistComplex>();
        public bool NoResults { get; set; }
    }

   

}

