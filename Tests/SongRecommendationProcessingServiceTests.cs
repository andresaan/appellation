using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using Data.Results;
using Moq;
using Spotify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class SongRecommendationProcessingServiceTests
    {
        private readonly ISongRecommendationProcessingService _sut;
        private readonly Mock<ISongRecommendationService> _songRecommendationService;
        private readonly Mock<ISearchSpotifyService> _searchSpotifyService;

        public SongRecommendationProcessingServiceTests()
        {
            _songRecommendationService = new Mock<ISongRecommendationService>();
            _searchSpotifyService = new Mock<ISearchSpotifyService>();

            _sut = new SongRecommendationProcessingService(_searchSpotifyService.Object, _songRecommendationService.Object);
        }

        [Fact]
        public async Task ProcessSongRecommendationsAsync_AllSeedsEmpty_ThrowArgumentNullExeption()
        {
            //arrange

            //act
            var act = async () => await _sut.ProcessSongRecommendationsAsync(null, null, null, 20, -1);

            //arrange
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(act);
            Assert.Equal("All seed values are null", exception.Message);
        }

        [Fact]
        public async Task ProcessSongRecommendationsAsync_RecommendedTracksIsEmpty_ThrowRecommendedTracksNullException()
        {
            //arrange
            var SongRecommendationsResult = new SongRecommendationsResult();
            
            _songRecommendationService.Setup(x => x.GetSongRecommendationsAsync(""))
                .Returns(Task.FromResult(SongRecommendationsResult));

            //act 
            var act = async () => await _sut.ProcessSongRecommendationsAsync("4NHQUGzhtTLFvgF5SZesLK", "0c6xIDDpzE81m2q797ordA", "classical", 0, 0);

            //assert
            var exception = await Assert.ThrowsAsync<RecommendedTracksNullException>(act);
            Assert.Equal("No recommendations returned", exception.Message);
        }

    }
}
