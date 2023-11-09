using Application.Interfaces;
using Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Application.Handlers
{
    public class FavoritesHandler : IFavoritesHandler
    {
        public List<Track> addTrackToFavorites(List<Track> favorites, Track track)
        {
                
            return new List<Track> { track };
        }

            //return favorites.Contains(track) ? favorites : favorites.Append(track).ToList();
        

    }
}
