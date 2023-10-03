using Application.Dtos;
using Data.Results;

namespace Application.Interfaces
{
    public interface ISongRecommendationHandler
    {
        public Task<SeedVerificationDto> VerifySeedInputsAsync(SongRecommendationsDto seedInput);

        public Task<Track[]> GetSongRecommendationsAsync(SeedVerificationDto seedVerifications);

        public Task<Track[]> TestGetSongRecommendationsAsync(SeedVerificationDto seedVerifications);
    }
}
