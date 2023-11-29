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
            if (favorites == null)
            {
                return new List<Track>()
                {
                    track
                };
            }

            else
            {
                return favorites.Append(track).ToList();
            }
        }

        public List<Track> removeTrackFromFavorites(List<Track> favorites, Track track)
        {

            foreach (Track item in favorites)
            {
                if (item.Id == track.Id)
                {
                    var contains = favorites.Contains(item);
                    favorites.Remove(item);
                    break;
                }
            }

            return favorites;
        }
    }
}
