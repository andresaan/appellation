using Data.Results;
using Data.Seed;

namespace Application.Interfaces
{
    public interface ISongRecommendationProcessingService
    {
        public Task<SongRecommendationSeeds> VerifySeedInputsAsync(string? artistInput, string? trackInput, string? genreInput);
        public Task<Track[]> ProcessSongRecommendationsAsync(string? artistInput, string? trackInput, string? genreInput, int limit, int popularityTarget);
    }
}
