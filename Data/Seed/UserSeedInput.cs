using Data.Results;
using System.ComponentModel;

namespace Data.Seed
{
    public class SongRecommendationSeeds
    {
        public List<ArtistSeedIntermediary> ArtistSeedIntermediaries { get; set; } = new List<ArtistSeedIntermediary>();
        public List<TrackSeedIntermediary> TrackSeedIntermediaries { get; set; } = new List<TrackSeedIntermediary>();
        public string? GenreVerifiedSeeds { get; set; } = string.Empty;
        public void CreateSeedIntermediaries(string? artistInput, string? trackInput, string? genreInput)
        {
            if (artistInput != null)
            {
                var splitArtistSeeds = artistInput.TrimEnd(',').Split(',').ToList();

                foreach (string artist in splitArtistSeeds)
                {
                    ArtistSeedIntermediaries.Add(new ArtistSeedIntermediary()
                    {
                        UserInput = artist,
                        SeedType = "artist"
                    });
                }
            }

            if (trackInput != null)
            {
                var splitTrackSeeds = trackInput.TrimEnd(',').Split(',').ToList();

                foreach (string track in splitTrackSeeds)
                {
                    TrackSeedIntermediaries.Add(new TrackSeedIntermediary()
                    {
                        UserInput = track,
                        SeedType = "track"
                    });
                }
            }

            GenreVerifiedSeeds = genreInput != null ? genreInput.TrimEnd(',') : "";
        }
    }
    public abstract class AbstractSeedIntermediary
    {
        public string UserInput { get; set; } = string.Empty;
        public string SeedType { get; set; } = string.Empty;
        public bool NoResults { get; set; }
    }
    public abstract class AbstractPotentialSeed
    {
        public string SpotifyId { get; set; } = string.Empty;
        
        public string SeedType = string.Empty;
    }
    public class ArtistSeedIntermediary : AbstractSeedIntermediary // NEW NAME - ArtistSeedIntermediary
    {
        public List<ArtistPotentialSeed> PotentialSeeds { get; set; } = new List<ArtistPotentialSeed>();
        public void ProcessArtistSearchSummary(ArtistSearchSummary searchSummary)
        {
            var potentialSeeds = new List<ArtistPotentialSeed>();

            foreach (ArtistComplex artist in searchSummary.Artists)
            {
                potentialSeeds.Add(new ArtistPotentialSeed()
                {
                    ArtistName = artist.Name,
                    Images = artist.Images,
                    SeedType = artist.Type,
                    SpotifyId = artist.Id

                });
            }

            NoResults = searchSummary.NoResults;
            PotentialSeeds = potentialSeeds;
        }
    }
    public class ArtistPotentialSeed : AbstractPotentialSeed
    {
        public Image[] Images { get; set; } = Array.Empty<Image>();
        public string ArtistName { get; set; } = string.Empty;
    }
    public class TrackSeedIntermediary : AbstractSeedIntermediary
    {
        public List<TrackPotentialSeed> PotentialSeeds { get; set; } = new List<TrackPotentialSeed>();
        public void ProcessTrackSearchSummary(TrackSearchSummary searchSummary)
        {
            var potentialSeeds = new List<TrackPotentialSeed>();

            foreach (Track track in searchSummary.Tracks)
            {
                potentialSeeds.Add(new TrackPotentialSeed()
                {
                    TrackName = track.Name,
                    PerformingArtist = string.Join(", ", track.PerformingArtists.Select(o => o.Name)),
                    SpotifyId = track.Id,
                    SeedType = track.Type,
                    Images = track.Album.Images
                }); ;
            }

            NoResults = searchSummary.NoResults;
            PotentialSeeds = potentialSeeds;
        }
    }
    public class TrackPotentialSeed : AbstractPotentialSeed
    {
        public string TrackName { get; set; } = string.Empty;
        public string PerformingArtist { get; set; } = string.Empty;
        public Image[] Images { get; set; } = Array.Empty<Image>();
    }
}


