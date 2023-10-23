using Data.Results;

namespace Appellation.Models
{
    public class SongRecommendationsIndexModel
    {
        
        public string? ArtistUserInput { get; set; } = string.Empty;
        public string? TrackUserInput { get; set; } = string.Empty;
        public string? GenreUserInput { get; set; } = string.Empty;
        public int Limit { get; set; } = 20;

        public bool RecommendationsGiven = false;
        public Track[] Tracks { get; set; } = Array.Empty<Track>();
        
        //add all other parameters
    }
}
