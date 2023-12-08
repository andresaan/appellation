using Application.Interfaces;
using Data.Results;

namespace Application.Handlers
{
    public class FavoritesHandler : IFavoritesHandler
    {
        public List<Track> CheckFavorites(List<Track> favorites)
        {
            if (favorites == null)
            {
                return new List<Track>();
            }

            return favorites;
        }

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
