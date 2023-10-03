using Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class SeedVerificationDto
    {
        public List<SeedIntermediary> SeedIntermediaries { get; set; } = new List<SeedIntermediary>();
        public List<TrackSeedIntermediary> TrackSeedIntermediaries { get; set; } = new List<TrackSeedIntermediary>();
        public List<VerifiedSeed> VerifiedSeeds { get; set; } = new List<VerifiedSeed>()
        {
            new VerifiedSeed(),
            new VerifiedSeed(),
            new VerifiedSeed(),
            new VerifiedSeed(),
            new VerifiedSeed()
        };
    }
}
