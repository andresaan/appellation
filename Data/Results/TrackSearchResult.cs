using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Results
{
    public class TrackSearchResult
    {
        [JsonProperty("tracks")]
        public TrackSearchSummary Summary { get; set; } = new TrackSearchSummary();
    }

    public class TrackSearchSummary
    {
        [JsonProperty("items")]
        public Track[] Tracks { get; set; } = Array.Empty<Track>();
        public bool NoResults { get; set; }

    }

}
