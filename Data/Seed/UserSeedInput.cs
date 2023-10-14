using Data.Results;
using System.ComponentModel;

namespace Data.Seed
{
    public class SongRecommendationSeeds
    {
        public List<ArtistSeedIntermediary> SeedIntermediaries { get; set; } = new List<ArtistSeedIntermediary>();
        public List<TrackSeedIntermediary> TrackSeedIntermediaries { get; set; } = new List<TrackSeedIntermediary>();
        public string? GenreVerifiedSeeds { get; set; } = string.Empty;
    }

    public abstract class AbstractSeedIntermediary
    {
        public string UserInput { get; set; } = string.Empty;
        public string SeedType { get; set; } = string.Empty;
    }

    public abstract class AbstractPotentialSeed
    {
        public string SpotifyId { get; set; } = string.Empty;
        
        public string SeedType = string.Empty;
    }
    public class ArtistSeedIntermediary : AbstractSeedIntermediary // NEW NAME - ArtistSeedIntermediary
    {
        public List<ArtistPotentialSeed> PotentialSeeds { get; set; } = new List<ArtistPotentialSeed>();
    }

    public class ArtistPotentialSeed : AbstractPotentialSeed
    {
        public Image[] Images { get; set; } = Array.Empty<Image>();
        public string ArtistName { get; set; } = string.Empty;
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

}


