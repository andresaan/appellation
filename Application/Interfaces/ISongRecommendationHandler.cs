
using Data.Results;
using Data.Seed;

namespace Application.Interfaces
{
    public interface ISongRecommendationHandler
    {
        public Task<SongRecommendationSeeds> VerifySeedInputsAsync(string? artistInput, string? trackInput, string? genreInput);

        public Task<Track[]> GetSongRecommendationsAsync(string? artistInput, string? trackInput, string? genreInput);
    }
}
