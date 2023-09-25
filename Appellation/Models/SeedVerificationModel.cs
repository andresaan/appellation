using Data.Seed;

namespace Appellation.Models
{
    public class SeedVerificationModel
    {
        public string ArtistUserInput { get; set; } = string.Empty;
        public List<SeedIntermediary> Intermediaries { get; set; } = new List<SeedIntermediary>();
        public string VerifiedSeeds { get; set; } = string.Empty;
    }
}
