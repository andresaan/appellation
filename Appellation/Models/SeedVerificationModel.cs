using Data.Seed;

namespace Appellation.Models
{
    public class SeedVerificationModel
    {
        public List<SeedIntermediary> SeedIntermediaries { get; set; } = new List<SeedIntermediary>();
        public List<TrackSeedIntermediary> TrackSeedIntermediaries { get; set; } = new List<TrackSeedIntermediary>();

        public string[] TArtistVerifiedSeeds { get; set; } = new string[5];
        public List<VerifiedSeed> ArtistVerifiedSeeds { get; set; } = new List<VerifiedSeed>()
        {
            new VerifiedSeed(),
            new VerifiedSeed(),
            new VerifiedSeed(),
            new VerifiedSeed(),
            new VerifiedSeed()
        };

        public List<VerifiedSeed> TrackVerifiedSeeds { get; set; } = new List<VerifiedSeed>
        {
            new VerifiedSeed(),
            new VerifiedSeed(),
            new VerifiedSeed(),
            new VerifiedSeed(),
            new VerifiedSeed()
        };

    }
}


//LOOK INTO REMOVING THE INTERMEDIARY FROM CAPTURE - USE THE INTERMEDIARY TO DISPLAY THE POTENTIAL SEEDS, BUT
// ONLY CAPTURE POTENTIAL SEEDS FROM USER. 