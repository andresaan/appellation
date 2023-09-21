using Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Interfaces
{
    public interface ISeedService
    {
        public void ProcessSeeds(SongRecSeed seedInput);

        public Task<List<PotentialSeed>> SearchIntermediariesAsync(SongRecSeed seedInput);

        //public void AddVerifiedSeed(SeedValue verifiedSeed);

    }

}