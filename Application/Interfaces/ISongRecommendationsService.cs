using Data.Results;

namespace Application.Interfaces
{
    public interface ISongRecommendationsService
    {
        public Task<Track[]> GetSongRecommendationsAsync(string queryParameters);
    }
}
