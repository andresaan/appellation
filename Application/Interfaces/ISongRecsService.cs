using Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISongRecsService
    {
        public Task<IEnumerable<Track>> GetSongRecsAsync(IEnumerable<string> seedTrack, IEnumerable<string> seedArtist, IEnumerable<string> seedGenre);
    }
}
