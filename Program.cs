using System;
using ArtGalleryManagement.Models;
using ArtGalleryManagement.Repositories;

namespace ArtGalleryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("    ART GALLERY MANAGEMENT SYSTEM");
            Console.WriteLine("=========================================");
            Console.WriteLine();

            var dbHelper = new DatabaseHelper();
            var repository = new ArtGalleryRepository(dbHelper);

            Console.Write("Would you like to seed the database with sample data? (y/n): ");
            var seedOption = Console.ReadLine();
            if (seedOption?.ToLower() == "y")
            {
                SeedData.SeedDatabase(repository);
            }

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== MAIN MENU ===");
                Console.WriteLine("1.  View All Artists");
                Console.WriteLine("2.  View All Genres");
                Console.WriteLine("3.  View All Exhibitions");
                Console.WriteLine("4.  View All Artworks");
                Console.WriteLine("5.  View All Entries");
                Console.WriteLine("6.  Add New Artist");
                Console.WriteLine("7.  Add New Artwork");
                Console.WriteLine("8.  Add New Exhibition");
                Console.WriteLine("9.  Add New Entry");
                Console.WriteLine("10. Query: Artworks with Artist and Genre");
                Console.WriteLine("11. Query: Artists with Artwork Count");
                Console.WriteLine("12. Query: Exhibitions with Entry Count");
                Console.WriteLine("13. Query: Genres with > 2 Artworks");
                Console.WriteLine("14. Query: Artists with > 1 Artwork");
                Console.WriteLine("15. Query: Exhibitions with > 3 Artworks");
                Console.WriteLine("16. Query: Artworks in Exhibitions");
                Console.WriteLine("17. Query: Artworks Not in Exhibition");
                Console.WriteLine("18. Query: Exhibitions and Their Artworks");
                Console.WriteLine("19. Query: Artists with Artworks and Exhibitions");
                Console.WriteLine("20. Query: Artists Total Artworks and Exhibitions");
                Console.WriteLine("21. Delete Artist");
                Console.WriteLine("22. Delete Artwork");
                Console.WriteLine("23. Exit");
                Console.Write("\nSelect an option (1-23): ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewAllArtists(repository); break;
                    case "2": ViewAllGenres(repository); break;
                    case "3": ViewAllExhibitions(repository); break;
                    case "4": ViewAllArtworks(repository); break;
                    case "5": ViewAllEntries(repository); break;
                    case "6": AddNewArtist(repository); break;
                    case "7": AddNewArtwork(repository); break;
                    case "8": AddNewExhibition(repository); break;
                    case "9": AddNewEntry(repository); break;
                    case "10": QueryArtworksWithArtistAndGenre(repository); break;
                    case "11": QueryArtistsWithArtworkCount(repository); break;
                    case "12": QueryExhibitionsWithEntryCount(repository); break;
                    case "13": QueryGenresWithMoreThanTwoArtworks(repository); break;
                    case "14": QueryArtistsWithMoreThanOneArtwork(repository); break;
                    case "15": QueryExhibitionsWithMoreThanThreeArtworks(repository); break;
                    case "16": QueryArtworksInExhibitions(repository); break;
                    case "17": QueryArtworksNotInExhibition(repository); break;
                    case "18": QueryExhibitionsAndTheirArtworks(repository); break;
                    case "19": QueryArtistsWithArtworksAndExhibitions(repository); break;
                    case "20": QueryArtistsWithTotalArtworksAndExhibitions(repository); break;
                    case "21": DeleteArtist(repository); break;
                    case "22": DeleteArtwork(repository); break;
                    case "23":
                        exit = true;
                        Console.WriteLine("\nThank you for using Art Gallery Management System!");
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // ==================== VIEW METHODS ====================

        static void ViewAllArtists(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ALL ARTISTS ===");
            var artists = repository.GetAllArtists();
            if (artists.Count == 0)
            {
                Console.WriteLine("No artists found.");
                return;
            }
            foreach (var artist in artists)
            {
                Console.WriteLine($"ID: {artist.ArtistID,-3} | Name: {artist.FullName}");
            }
            Console.WriteLine($"Total: {artists.Count} artists");
        }

        static void ViewAllGenres(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ALL GENRES ===");
            var genres = repository.GetAllGenres();
            if (genres.Count == 0)
            {
                Console.WriteLine("No genres found.");
                return;
            }
            foreach (var genre in genres)
            {
                Console.WriteLine($"ID: {genre.GenreID,-3} | Description: {genre.Description}");
            }
            Console.WriteLine($"Total: {genres.Count} genres");
        }

        static void ViewAllExhibitions(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ALL EXHIBITIONS ===");
            var exhibitions = repository.GetAllExhibitions();
            if (exhibitions.Count == 0)
            {
                Console.WriteLine("No exhibitions found.");
                return;
            }
            foreach (var exhibition in exhibitions)
            {
                Console.WriteLine($"ID: {exhibition.ExhibitionID,-3} | Description: {exhibition.Description}");
            }
            Console.WriteLine($"Total: {exhibitions.Count} exhibitions");
        }

        static void ViewAllArtworks(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ALL ARTWORKS ===");
            var artworks = repository.GetAllArtworks();
            if (artworks.Count == 0)
            {
                Console.WriteLine("No artworks found.");
                return;
            }
            foreach (var artwork in artworks)
            {
                Console.WriteLine($"ID: {artwork.ArtworkID,-3} | Title: {artwork.Title,-35} | GenreID: {artwork.GenreID} | ArtistID: {artwork.ArtistID}");
            }
            Console.WriteLine($"Total: {artworks.Count} artworks");
        }

        static void ViewAllEntries(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ALL ENTRIES ===");
            var entries = repository.GetAllEntries();
            if (entries.Count == 0)
            {
                Console.WriteLine("No entries found.");
                return;
            }
            foreach (var entry in entries)
            {
                Console.WriteLine($"EntryID: {entry.EntryID,-3} | ArtworkID: {entry.ArtworkID} | ExhibitionID: {entry.ExhibitionID}");
            }
            Console.WriteLine($"Total: {entries.Count} entries");
        }

        // ==================== ADD METHODS ====================

        static void AddNewArtist(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ADD NEW ARTIST ===");
            Console.Write("Enter artist name: ");
            var name = Console.ReadLine();
            Console.Write("Enter artist surname: ");
            var surname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
            {
                Console.WriteLine("Name and surname cannot be empty!");
                return;
            }

            var artist = new Artist { Name = name.Trim(), Surname = surname.Trim() };
            repository.AddArtist(artist);
            Console.WriteLine($"✅ Artist '{name} {surname}' added successfully!");
        }

        static void AddNewArtwork(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ADD NEW ARTWORK ===");
            Console.Write("Enter artwork title: ");
            var title = Console.ReadLine();
            Console.Write("Enter GenreID: ");
            if (!int.TryParse(Console.ReadLine(), out int genreId))
            {
                Console.WriteLine("Invalid GenreID!");
                return;
            }
            Console.Write("Enter ArtistID: ");
            if (!int.TryParse(Console.ReadLine(), out int artistId))
            {
                Console.WriteLine("Invalid ArtistID!");
                return;
            }

            var artwork = new Artwork { Title = title, GenreID = genreId, ArtistID = artistId };
            repository.AddArtwork(artwork);
            Console.WriteLine($"✅ Artwork '{title}' added successfully!");
        }

        static void AddNewExhibition(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ADD NEW EXHIBITION ===");
            Console.Write("Enter exhibition description: ");
            var description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Description cannot be empty!");
                return;
            }

            var exhibition = new Exhibition { Description = description.Trim() };
            repository.AddExhibition(exhibition);
            Console.WriteLine($"✅ Exhibition '{description}' added successfully!");
        }

        static void AddNewEntry(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ADD NEW ENTRY ===");
            Console.Write("Enter ArtworkID: ");
            if (!int.TryParse(Console.ReadLine(), out int artworkId))
            {
                Console.WriteLine("Invalid ArtworkID!");
                return;
            }
            Console.Write("Enter ExhibitionID: ");
            if (!int.TryParse(Console.ReadLine(), out int exhibitionId))
            {
                Console.WriteLine("Invalid ExhibitionID!");
                return;
            }

            var entry = new Entry { ArtworkID = artworkId, ExhibitionID = exhibitionId };
            repository.AddEntry(entry);
            Console.WriteLine($"✅ Entry added successfully! Artwork {artworkId} linked to Exhibition {exhibitionId}");
        }

        // ==================== QUERY METHODS ====================

        static void QueryArtworksWithArtistAndGenre(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ARTWORKS WITH ARTIST AND GENRE ===");
            var results = repository.GetArtworksWithArtistAndGenre();
            if (results.Count == 0)
            {
                Console.WriteLine("No artworks found.");
                return;
            }
            Console.WriteLine("Title".PadRight(35) + "Artist".PadRight(25) + "Genre");
            Console.WriteLine(new string('-', 75));
            foreach (var item in results)
            {
                Console.WriteLine($"{item.ArtworkTitle,-35} {item.Artist,-25} {item.Genre}");
            }
            Console.WriteLine($"Total: {results.Count} artworks");
        }

        static void QueryArtistsWithArtworkCount(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ARTISTS WITH ARTWORK COUNT ===");
            var results = repository.GetArtistsWithArtworkCount();
            if (results.Count == 0)
            {
                Console.WriteLine("No artists found.");
                return;
            }
            Console.WriteLine("Artist".PadRight(35) + "Artwork Count");
            Console.WriteLine(new string('-', 50));
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Artist,-35} {item.ArtworkCount}");
            }
        }

        static void QueryExhibitionsWithEntryCount(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== EXHIBITIONS WITH ENTRY COUNT ===");
            var results = repository.GetExhibitionsWithEntryCount();
            if (results.Count == 0)
            {
                Console.WriteLine("No exhibitions found.");
                return;
            }
            Console.WriteLine("Exhibition".PadRight(35) + "Entry Count");
            Console.WriteLine(new string('-', 50));
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Exhibition,-35} {item.EntryCount}");
            }
        }

        static void QueryGenresWithMoreThanTwoArtworks(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== GENRES WITH MORE THAN 2 ARTWORKS ===");
            var results = repository.GetGenresWithMoreThanTwoArtworks();
            if (results.Count == 0)
            {
                Console.WriteLine("No genres found with more than 2 artworks.");
                return;
            }
            Console.WriteLine("Genre".PadRight(25) + "Artwork Count");
            Console.WriteLine(new string('-', 40));
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Genre,-25} {item.ArtworkCount}");
            }
        }

        static void QueryArtistsWithMoreThanOneArtwork(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ARTISTS WITH MORE THAN 1 ARTWORK ===");
            var results = repository.GetArtistsWithMoreThanOneArtwork();
            if (results.Count == 0)
            {
                Console.WriteLine("No artists found with more than 1 artwork.");
                return;
            }
            Console.WriteLine("Artist".PadRight(35) + "Artwork Count");
            Console.WriteLine(new string('-', 50));
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Artist,-35} {item.ArtworkCount}");
            }
        }

        static void QueryExhibitionsWithMoreThanThreeArtworks(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== EXHIBITIONS WITH MORE THAN 3 ARTWORKS ===");
            var results = repository.GetExhibitionsWithMoreThanThreeArtworks();
            if (results.Count == 0)
            {
                Console.WriteLine("No exhibitions found with more than 3 artworks.");
                return;
            }
            Console.WriteLine("Exhibition".PadRight(35) + "Artwork Count");
            Console.WriteLine(new string('-', 50));
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Exhibition,-35} {item.ArtworkCount}");
            }
        }

        static void QueryArtworksInExhibitions(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ARTWORKS IN EXHIBITIONS ===");
            var results = repository.GetArtworksInExhibitions();
            if (results.Count == 0)
            {
                Console.WriteLine("No artworks found in exhibitions.");
                return;
            }
            Console.WriteLine("Artwork".PadRight(30) + "Artist".PadRight(25) + "Genre".PadRight(20) + "Exhibition");
            Console.WriteLine(new string('-', 100));
            foreach (var item in results)
            {
                Console.WriteLine($"{item.ArtworkTitle,-30} {item.Artist,-25} {item.Genre,-20} {item.Exhibition}");
            }
        }

        static void QueryArtworksNotInExhibition(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ARTWORKS NOT IN EXHIBITION ===");
            var results = repository.GetArtworksNotInExhibition();
            if (results.Count == 0)
            {
                Console.WriteLine("All artworks are in exhibitions.");
                return;
            }
            Console.WriteLine("Artwork".PadRight(30) + "Artist".PadRight(25) + "Genre".PadRight(20) + "Status");
            Console.WriteLine(new string('-', 100));
            foreach (var item in results)
            {
                Console.WriteLine($"{item.ArtworkTitle,-30} {item.Artist,-25} {item.Genre,-20} {item.Exhibition}");
            }
        }

        static void QueryExhibitionsAndTheirArtworks(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== EXHIBITIONS AND THEIR ARTWORKS ===");
            var results = repository.GetExhibitionsAndTheirArtworks();
            if (results.Count == 0)
            {
                Console.WriteLine("No exhibitions found.");
                return;
            }
            Console.WriteLine("Exhibition".PadRight(30) + "Artwork".PadRight(35) + "Artist");
            Console.WriteLine(new string('-', 90));
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Exhibition,-30} {item.ArtworkTitle,-35} {item.Artist}");
            }
        }

        static void QueryArtistsWithArtworksAndExhibitions(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ARTISTS WITH ARTWORKS AND EXHIBITIONS ===");
            var results = repository.GetArtistsWithArtworksAndExhibitions();
            if (results.Count == 0)
            {
                Console.WriteLine("No artists found.");
                return;
            }
            Console.WriteLine("Artist".PadRight(25) + "Artwork".PadRight(30) + "Genre".PadRight(20) + "Exhibition");
            Console.WriteLine(new string('-', 100));
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Artist,-25} {item.Artwork,-30} {item.Genre,-20} {item.Exhibition}");
            }
        }

        static void QueryArtistsWithTotalArtworksAndExhibitions(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== ARTISTS WITH TOTAL ARTWORKS AND EXHIBITIONS ===");
            var results = repository.GetArtistsWithTotalArtworksAndExhibitions();
            if (results.Count == 0)
            {
                Console.WriteLine("No artists found.");
                return;
            }
            Console.WriteLine("Artist".PadRight(30) + "Total Artworks".PadRight(20) + "Total Exhibitions");
            Console.WriteLine(new string('-', 70));
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Artist,-30} {item.TotalArtworks,-20} {item.TotalExhibitions}");
            }
        }

        // ==================== DELETE METHODS ====================

        static void DeleteArtist(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== DELETE ARTIST ===");
            Console.Write("Enter ArtistID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID!");
                return;
            }
            repository.DeleteArtist(id);
            Console.WriteLine($"✅ Artist with ID {id} and all related data deleted successfully!");
        }

        static void DeleteArtwork(ArtGalleryRepository repository)
        {
            Console.WriteLine("\n=== DELETE ARTWORK ===");
            Console.Write("Enter ArtworkID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID!");
                return;
            }
            repository.DeleteArtwork(id);
            Console.WriteLine($"✅ Artwork with ID {id} and all related entries deleted successfully!");
        }
    }
}