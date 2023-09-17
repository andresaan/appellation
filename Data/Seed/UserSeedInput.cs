using System.ComponentModel;

namespace Data.Seed
{
    public class SeedInput
    {
        public string? ArtistUserInput { get; set; } = string.Empty;
        public string? TrackUserInput { get; set; } = string.Empty;
        public string? GenreUserInput { get; set; } = string.Empty;

        public List<PotentialSeed>? PotentialArtistSeeds { get; set; } = new List<PotentialSeed>();


        public List<SeedValue>? potentialTrackSeeds { get; set; } = new List<SeedValue>();
        public List<SeedValue>? PotentialGenreSeeds { get; set; } = new List<SeedValue>();

        public List<SeedValue>? VerifiedArtistSeeds { get; set; } = new List<SeedValue>();
        public List<SeedValue>? VerifiedTrackSeeds { get; set; } = new List<SeedValue>();
        public List<SeedValue>? VerifiedGenreSeeds { get; set; } = new List<SeedValue>();

    }

    public class SeedValue
    {
        public string UserInput { get; set; } = string.Empty;
        public string SeedType { get; set; } = string.Empty;
        public string VerifiedName { get; set; } = string.Empty;
        public string VerifiedSpotifyId { get; set; } = string.Empty;
    }

    public class PotentialSeed
    {
        public string UserInput { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SpotifyId { get; set; } = string.Empty;

    }
}
