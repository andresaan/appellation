using Data.Seed;
using Application.Interfaces;
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
        public void ProcessSeeds(SongRecSeed seedInput)
        {
            if (seedInput.ArtistUserInput != null)
            {
                List<string> splitArtistSeeds = seedInput.ArtistUserInput.Split(',').ToList();

                foreach(string artist in splitArtistSeeds)
                {
                    seedInput.SeedIntermediaries.Add(new SeedIntermediary()
                    {
                        UserInput = artist,
                        SeedType = "artist"
                    });
                }  
            }
            

        }

        public async Task<List<PotentialSeed>> SearchIntermediariesAsync(SongRecSeed seedInput)
        {
            //GetPotentialSeedsAsync
            throw new NotImplementedException();
        }

        public void AddVerifiedSeed(VerifiedSeed verifiedSeed)
        {
            throw new NotImplementedException();
        }

    }
}
