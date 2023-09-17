using Spotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Services
{
    public interface ISongRecs
    {
        public Task<IEnumerable<Track>> GetSongRecsAsync(IEnumerable<string> seedTrack, IEnumerable<string> seedArtist, IEnumerable<string> seedGenre);
    }
}
