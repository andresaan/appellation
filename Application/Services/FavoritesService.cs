using Application.Interfaces;
using Data.Results;

namespace Application.Services
{
    public class FavoritesService : IFavoritesService
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
            // Do I need to handle null warning if you cant "remove" without something being in the list anyways?

            favorites.Remove(favorites.Find(o => o.Id == track.Id));

            //foreach (Track item in favorites)
            //{
            //    if (item.Id == track.Id)
            //    {
            //        favorites.Remove(item);
            //        break;
            //    }
            //}

            return favorites;
        }
    }
}
