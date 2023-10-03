using Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class SongRecommendationsDto
    {
        public string? ArtistUserInput { get; set; } = string.Empty;
        public string? TrackUserInput { get; set; } = string.Empty;
        public string? GenreUserInput { get; set; } = string.Empty;


        public bool RecommendationsGiven = false;
        public Track[] Tracks { get; set; } = Array.Empty<Track>();
    }
}
