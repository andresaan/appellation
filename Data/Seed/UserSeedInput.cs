using Data.Results;
using System.ComponentModel;

namespace Data.Seed
{
    public class SongRecommendationSeeds
    {
        public List<VerifiedSeed> ArtistVerifiedSeeds { get; set; } = new List<VerifiedSeed>();
        public List<VerifiedSeed> TrackVerifiedSeeds { get; set; } = new List<VerifiedSeed>();
        public string ArtistSeedQueryParameter { get; set; } = string.Empty;
        public string TrackSeedQueryParameter { get; set; } = string.Empty;

        public string[] TArtistVerifiedSeeds { get; set; } = Array.Empty<string>();

    }

    public abstract class AbstractSeedIntermediary
    {
        public string UserInput { get; set; } = string.Empty;
        public bool Error { get; set; }
        public string SeedType { get; set; } = string.Empty;
    }

    public abstract class AbstractPotentialSeed
    {
        public string SpotifyId { get; set; } = string.Empty;
        public bool Verified { get; set; }  
        public string SeedType = string.Empty;
    }
    public class SeedIntermediary // NEW NAME - ArtistSeedIntermediary
    {
        public string UserInput { get; set; } = string.Empty;
        public List<PotentialSeed> PotentialSeeds { get; set; } = new List<PotentialSeed>();
        public bool Error { get; set; }
        public string SeedType { get; set; } = string.Empty;
    }

    public class PotentialSeed // NEW NAME - ArtistPotentialSeed
    {
        public Image[] Images { get; set; } = Array.Empty<Image>();
        public string ArtistName { get; set; } = string.Empty;
        public string SpotifyId { get; set; } = string.Empty;
        public bool Verified { get; set; }  // test fielf for verification form
        public string SeedType = string.Empty;
    }

    public class TrackSeedIntermediary : AbstractSeedIntermediary
    {
        public List<TrackPotentialSeed> TrackPotentialSeeds { get; set; } = new List<TrackPotentialSeed>();
    }
    
    public class TrackPotentialSeed : AbstractPotentialSeed
    {
        public string TrackName { get; set; } = string.Empty;
        public string PerformingArtist { get; set; } = string.Empty;
        public Image[] Images { get; set; } = Array.Empty<Image>();
    }

    public class VerifiedSeed
    {
        public string SpotifyId { get; set; } = string.Empty;
        public string SeedType { get; set; } = string.Empty;
    }

}


