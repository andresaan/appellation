using Application.Models.SongRecommendations;
using Data.Results;
using Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProcessSongRecommendations
    {
        public Task<SeedVerificationModel> VerifySeedInputsAsync(SongRecommendationsIndexModel seedInput);

        public Task<Track[]> GetSongRecommendationsAsync(SeedVerificationModel seedVerifications);
    }
}
