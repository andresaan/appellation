using Data.Seed;
using Application.Interfaces;
using Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.SongRecommendations;

namespace Spotify.Services
{
    public class SeedService : ISeedService
    {

        public SeedVerificationModel ProcessSeeds(SongRecommendationsIndexModel seedInput)
        {
            var songRecommendationSeeds = new SeedVerificationModel();

            if (seedInput.ArtistUserInput != null)
            {
                var splitArtistSeeds = seedInput.ArtistUserInput.Split(',').ToList();

                foreach (string artist in splitArtistSeeds)
                {
                    songRecommendationSeeds.SeedIntermediaries.Add(new SeedIntermediary()
                    {
                        UserInput = artist,
                        SeedType = "artist"
                    });
                }
            }

            return songRecommendationSeeds;
        }  
        
        //public void ProcessSeeds(SongRecSeed seedInput)
        //{
        //    if (seedInput.ArtistUserInput != null)
        //    {
        //        List<string> splitArtistSeeds = seedInput.ArtistUserInput.Split(',').ToList();

        //        foreach(string artist in splitArtistSeeds)
        //        {
        //            seedInput.SeedIntermediaries.Add(new SeedIntermediary()
        //            {
        //                UserInput = artist,
        //                SeedType = "artist"
        //            });
        //        }  
        //    }
        //}
    }
}
