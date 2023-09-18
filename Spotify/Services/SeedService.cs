using Data.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Services
{
    public class SeedService : ISeedService
    {
        public void ProcessSeeds(SeedInput seedInput)
        {
            if (seedInput.ArtistUserInput != null)
            {
                List<string> splitArtistSeeds = seedInput.ArtistUserInput.Split(',').ToList();

                foreach(string artist in splitArtistSeeds)
                {
                    seedInput.PotentialArtistSeeds.Add( new PotentialSeed()
                    {
                        UserInput = artist
                    });
                }  
            }

        }

        public List<PotentialSeed> GetPotentialSeedsAsync(List<SeedValue> seedsProvided)
        {
            throw new NotImplementedException();
        }


        public void AddVerifiedSeed(SeedValue verifiedSeed)
        {
            throw new NotImplementedException();
        }

        

    }
}
