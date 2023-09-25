using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Results
{
    public class SongRecommendationsResult
    {
        public Seed[] Seeds { get; set; } = Array.Empty<Seed>();
        public Track[] Tracks { get; set; } = Array.Empty<Track>();
    }

    public class Track
    {
        public Album Album { get; set; } = new Album();
        
        [JsonProperty("artists")]
        public ArtistComplex[] PerformingArtists { get; set; } = Array.Empty<ArtistComplex>();

        [JsonProperty("available_markets")]
        public string[] AvailableMarkets { get; set; } = Array.Empty<string>();

        [JsonProperty("disc_number")]
        public int DiscNumber { get; set; } 

        [JsonProperty("duration_ms")]
        public int DurationMs { get; set; }
        public bool Explicit { get; set; }

        [JsonProperty("external_ids")]
        public External_Ids ExternalIds { get; set; } = new External_Ids();

        [JsonProperty("external_urls")]
        public External_Urls ExternalUrls { get; set; } = new External_Urls();
        public string Href { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        [JsonProperty("is_playable")]
        public bool IsPlayable { get; set; }
        public Restrictions Restrictions { get; set; } = new Restrictions();
        public string Name { get; set; } = string.Empty;
        public int Popularity { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; } = string.Empty;

        [JsonProperty("track_number")]
        public int TrackNumber { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }
    }

    public class Album
    {
        [JsonProperty("album_type")]
        public string AlbumType { get; set; } = string.Empty;

        [JsonProperty("total_tracks")]
        public int TotalTracks { get; set; }

        [JsonProperty("available_markets")]
        public string[]? AvailableMarkets { get; set; }

        [JsonProperty("external_urls")]
        public External_Urls ExternalUrls { get; set; } = new External_Urls();
        public string Href { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public Image[] Images { get; set; } = Array.Empty<Image>();
        public string Name { get; set; } = string.Empty;

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;

        [JsonProperty("release_date_precision")]
        public string ReleaseDatePrecision { get; set; } = string.Empty;
        public Restrictions Restrictions { get; set; } = new Restrictions();
        public string Type { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;

        [JsonProperty("artists")]
        public ArtistSimple[] ContributingArtists { get; set; } = new ArtistSimple[0];
    }

    public class External_Urls
    {
        public string spotify { get; set; } = string.Empty;
    }

    public class Restrictions
    {
        public string Reason { get; set; } = string.Empty;
    }

    public class Image
    {
        public string Url { get; set; } = string.Empty;
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class External_Ids
    {
        public string Isrc { get; set; } = string.Empty;
        public string Ean { get; set; } = string.Empty;
        public string Upc { get; set; } = string.Empty;
    }

    public class Followers
    {
        public string Href { get; set; } = string.Empty;
        public int Total { get; set; }
    }

    public class Seed
    {
        public int AfterFilteringSize { get; set; }
        public int AfterRelinkingSize { get; set; }
        public string Href { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public int InitialPoolSize { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}