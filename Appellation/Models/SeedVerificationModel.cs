using Data.Seed;

namespace Appellation.Models
{
    public class SeedVerificationModel
    {
        public List<SeedIntermediary> SeedIntermediaries { get; set; } = new List<SeedIntermediary>();
        public List<TrackSeedIntermediary> TrackSeedIntermediaries { get; set; } = new List<TrackSeedIntermediary>();
         public List<VerifiedSeed> VerifiedSeeds { get; set; } = new List<VerifiedSeed>()
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