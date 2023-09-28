using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Results
{
    // NOTES:
    // - Original Album was missing restrictions
    // - Original Track (item) was missing IsPlayable and Restrictions -

    public class TrackSearchResult
    {
        public TrackSearchSummary TrackSearchSummary { get; set; } = new TrackSearchSummary();
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
