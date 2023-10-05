using Data.Seed;

namespace Appellation.Models
{
    public class SeedVerificationModel
    {
        public List<SeedIntermediary> SeedIntermediaries { get; set; } = new List<SeedIntermediary>();
        public List<TrackSeedIntermediary> TrackSeedIntermediaries { get; set; } = new List<TrackSeedIntermediary>();

        public string[] ArtistVerifiedSeeds { get; set; } = new string[5];
        public string[] TrackVerifiedSeeds { get; set; } = new string[5];
        public string[] GenreVerifiedSeeds { get; set; } = new string[5];
    }
}


//LOOK INTO REMOVING THE INTERMEDIARY FROM CAPTURE - USE THE INTERMEDIARY TO DISPLAY THE POTENTIAL SEEDS, BUT
// ONLY CAPTURE POTENTIAL SEEDS FROM USER. 