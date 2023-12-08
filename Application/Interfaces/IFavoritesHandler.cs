using Data.Results;

namespace Application.Interfaces
{
    public interface IFavoritesHandler
    {
        public List<Track> CheckFavorites(List<Track> favorites);

        public List<Track> addTrackToFavorites(List<Track> favorites, Track track);

        public List<Track> removeTrackFromFavorites(List<Track> favorites, Track track);
    }
}
