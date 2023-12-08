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
        public string Href { get; set; } = string.Empty;
        public int Limit { get; set; }
        public string Next { get; set; } = string.Empty;
        public int Offset { get; set; }
        public object Previous { get; set; } = new object();
        public int Total { get; set; }
        [JsonProperty("items")]
        public Track[] Tracks { get; set; } = Array.Empty<Track>();
    }

}
