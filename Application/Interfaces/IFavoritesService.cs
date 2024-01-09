using Data.Results;

namespace Application.Interfaces
{
    public interface IFavoritesService
    {
        public List<Track> addTrackToFavorites(List<Track> favorites, Track track);

        public List<Track> removeTrackFromFavorites(List<Track> favorites, Track track);
    }
}
