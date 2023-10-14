using Data.Seed;

namespace Appellation.Models
{
    public class SeedVerificationModel
    {
        public List<ArtistSeedIntermediary> SeedIntermediaries { get; set; } = new List<ArtistSeedIntermediary>();
        public List<TrackSeedIntermediary> TrackSeedIntermediaries { get; set; } = new List<TrackSeedIntermediary>();
     
        public string? GenreVerifiedSeeds { get; set; } = string.Empty;
        public string? ArtistVerifiedSeeds { get; set; } = string.Empty;
        public string? TrackVerifiedSeeds { get; set; } = string.Empty;

    }
}


//LOOK INTO REMOVING THE INTERMEDIARY FROM CAPTURE - USE THE INTERMEDIARY TO DISPLAY THE POTENTIAL SEEDS, BUT
// ONLY CAPTURE POTENTIAL SEEDS FROM USER. 