using Data.Results;
using System.ComponentModel;

namespace Data.Seed
{
    public class SongRecommendationSeeds
    {
        public List<VerifiedSeed> VerifiedArtistSeeds { get; set; } = new List<VerifiedSeed>();
        public string ArtistSeedQueryParameter { get; set; } = string.Empty;
    }

    public class SeedIntermediary // ind group
    {
        public string UserInput { get; set; } = string.Empty;
        public List<PotentialSeed> PotentialSeeds { get; set; } = new List<PotentialSeed>();
        public bool Error { get; set; }
        public string SeedType { get; set; } = string.Empty;
    }

    public class PotentialSeed
    {
        public Image[] Images { get; set; } = Array.Empty<Image>();
        public string ArtistName { get; set; } = string.Empty;
        public string SpotifyId { get; set; } = string.Empty;
        public bool Verified { get; set; }  // test fielf for verification form
        public string SeedType = string.Empty;
    }

    public class VerifiedSeed
    {
        public string SpotifyId { get; set; } = string.Empty;
        public string SeedType { get; set; } = string.Empty;
    }

}


