using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGalleryManagement.Models;

namespace ArtGalleryManagement.Repositories
{
    /// Interface defining all repository operations for the Art Gallery database
    /// This provides a contract that the repository class must implement
    public interface IArtGalleryRepository
    {
        // ===== ARTIST OPERATIONS =====
        List<Artist> GetAllArtists();
        void AddArtist(Artist artist);
        void UpdateArtist(Artist artist);
        void DeleteArtist(int artistId);

        // ===== GENRE OPERATIONS =====
        List<Genre> GetAllGenres();
        void AddGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(int genreId);

        // ===== EXHIBITION OPERATIONS =====
        List<Exhibition> GetAllExhibitions();
        void AddExhibition(Exhibition exhibition);
        void UpdateExhibition(Exhibition exhibition);
        void DeleteExhibition(int exhibitionId);

        // ===== ARTWORK OPERATIONS =====
        List<Artwork> GetAllArtworks();
        void AddArtwork(Artwork artwork);
        void UpdateArtwork(Artwork artwork);
        void DeleteArtwork(int artworkId);

        // ===== ENTRY OPERATIONS =====
        List<Entry> GetAllEntries();
        void AddEntry(Entry entry);
        void DeleteEntry(int entryId);

        // ===== QUERY OPERATIONS =====
        List<dynamic> GetArtworksWithArtistAndGenre();
        int GetArtworkCountByGenre(string genreDescription);
        List<dynamic> GetArtistsWithArtworkCount();
        List<dynamic> GetExhibitionsWithEntryCount();
        List<dynamic> GetGenresWithMoreThanTwoArtworks();
        List<dynamic> GetArtistsWithMoreThanOneArtwork();
        List<dynamic> GetExhibitionsWithMoreThanThreeArtworks();
        List<dynamic> GetArtworksInExhibitions();
        List<dynamic> GetArtworksNotInExhibition();
        List<dynamic> GetExhibitionsAndTheirArtworks();
        List<dynamic> GetArtistsWithArtworksAndExhibitions();
        List<dynamic> GetArtistsWithTotalArtworksAndExhibitions();
    }
}