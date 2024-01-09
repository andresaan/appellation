using Data.Seed;

namespace Tests
{
    public class SongRecommendationSeedsTests
    {
        private readonly SongRecommendationSeeds _sut;

        public  SongRecommendationSeedsTests()
        {
            _sut = new SongRecommendationSeeds();
        }

        [Fact]
        public void CreateSeedIntermediaries_ArtistInputIsEmpty_ArtistSeedIntermediaryIsEmpty()
        {
            //arrange

            //act
            _sut.CreateSeedIntermediaries(null, "", "");

            //assert
            Assert.Empty(_sut.ArtistSeedIntermediaries);

        }
    }
}