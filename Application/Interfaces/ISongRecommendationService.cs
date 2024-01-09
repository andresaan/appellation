using Data.Results;

namespace Application.Interfaces
{
    public interface ISongRecommendationService
    {
        public Task<SongRecommendationsResult> GetSongRecommendationsAsync(string queryParameters);
    }
}
