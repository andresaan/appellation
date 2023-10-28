using Data.Seed;

namespace Appellation.Models
{
    public class SeedVerificationModel
    {
        public List<ArtistSeedIntermediary> ArtistSeedIntermediaries { get; set; } = new List<ArtistSeedIntermediary>();
        public List<TrackSeedIntermediary> TrackSeedIntermediaries { get; set; } = new List<TrackSeedIntermediary>();
     
        public string? GenreVerifiedSeeds { get; set; } = string.Empty;
        public string? ArtistVerifiedSeeds { get; set; } = string.Empty;
        public string? TrackVerifiedSeeds { get; set; } = string.Empty;

        public int Limit { get; set; } = 20;

    }
}