using Data.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Appellation.Models
{
    public class SongRecommendationsIndexModel
    {
        public string ArtistUserInput { get; set; } = string.Empty;
        public string TrackUserInput { get; set; } = string.Empty;
        public string GenreUserInput { get; set; } = string.Empty;
        public int Limit { get; set; } = 20;
        public int PopularityMax { get; set; } 
        public Track[] Tracks { get; set; } = Array.Empty<Track>();
    }
}
