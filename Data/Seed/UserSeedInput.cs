using Data.Results;
using System.ComponentModel;

namespace Data.Seed
{
    public class SongRecSeed
    {
        public string? ArtistUserInput { get; set; } = string.Empty;
        public string? TrackUserInput { get; set; } = string.Empty;
        public string? GenreUserInput { get; set; } = string.Empty;
        public List<SeedIntermediary> SeedIntermediaries { get; set; } = new List<SeedIntermediary>();
        public List<VerifiedSeed> VerifiedSeeds { get; set; }
        public string _verifiedSeedId { get; set; } = string.Empty;
    }

    public class SeedIntermediary // ind group
    {
        public string UserInput { get; set; } = string.Empty;
        public List<PotentialSeed> PotentialSeeds { get; set; } = new List<PotentialSeed>();
        public bool Error { get; set; }
        public string SeedType { get; set; } = string.Empty;
        public string VerifiedSeedId { get; set; } = string.Empty; // test fielf for verification form
    }

    public class PotentialSeed
    {
        public Image[] Images { get; set; } 
        public string ArtistName { get; set; } = string.Empty;
        public string SpotifyId { get; set; } = string.Empty;
        public bool Verified { get; set; }  // test fielf for verification form

    }
    public class VerifiedSeed
    {
        public string VerifiedName { get; set; } = string.Empty;
        public string VerifiedSpotifyId { get; set; } = string.Empty;
    }

}


