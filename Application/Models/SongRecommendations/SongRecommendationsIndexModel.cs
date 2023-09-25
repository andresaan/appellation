using Data.Results;

namespace Application.Models.SongRecommendations
{
    public class SongRecommendationsIndexModel
    {
        public string? ArtistUserInput { get; set; } = string.Empty;
        public string? TrackUserInput { get; set; } = string.Empty;
        public string? GenreUserInput { get; set; } = string.Empty;


        public bool RecommendationsGiven = false;
        public Track[] Tracks { get; set; } = Array.Empty<Track>();
        

        //add all other parameters
    }
}
