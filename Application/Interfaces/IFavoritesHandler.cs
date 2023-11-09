using Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFavoritesHandler
    {
        public List<Track> addTrackToFavorites(List<Track> favorites, Track track);
    }
}
