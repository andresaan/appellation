using Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Spotify.Services
{
    public interface ISeedService
    {
        public void ProcessSeeds(SeedInput seedInput);

        public List<PotentialSeed> GetPotentialSeedsAsync(List<SeedValue> seedsProvided);

        public void AddVerifiedSeed(SeedValue verifiedSeed);

    }

}