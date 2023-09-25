using Data.Seed;

namespace Application.Models.SongRecommendations
{
    public class SeedVerificationModel
    {
        public List<SeedIntermediary> SeedIntermediaries { get; set; } = new List<SeedIntermediary>();
        public string VerifiedSeeds { get; set; } = string.Empty;
    }
}
