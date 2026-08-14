using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGalleryManagement.Models;

namespace ArtGalleryManagement.Repositories
{
    /// SeedData class - populates the database with sample data for testing
    /// This class adds initial records to all tables so the database has data to query
    public class SeedData
    {
        /// Seeds the database with sample artists, genres, exhibitions, artworks, and entries
        /// <param name="repository">The repository to use for database operations</param>
        public static void SeedDatabase(ArtGalleryRepository repository)
        {
            Console.WriteLine("Seeding database with sample data...");

            // 1. SEED ARTISTS - Add famous artists to the Artist table
            Console.WriteLine("Adding artists...");
            var artists = new[]
            {
                new Artist { Name = "Michelangelo", Surname = "Buonarroti" },
                new Artist { Name = "Vincent", Surname = "van Gogh" },
                new Artist { Name = "Claude", Surname = "Monet" },
                new Artist { Name = "Pablo", Surname = "Picasso" },
                new Artist { Name = "Frida", Surname = "Kahlo" },
                new Artist { Name = "Leonardo", Surname = "da Vinci" },
                new Artist { Name = "Rembrandt", Surname = "van Rijn" },
                new Artist { Name = "Salvador", Surname = "Dali" }
            };
            // Loop through each artist and add to database
            foreach (var artist in artists) 
                repository.AddArtist(artist);

            // 2. SEED GENRES - Add art genre categories to the Genre table
            Console.WriteLine("Adding genres...");
            var genres = new[]
            {
                new Genre { Description = "Abstract" },
                new Genre { Description = "Impressionism" },
                new Genre { Description = "Surrealism" },
                new Genre { Description = "Cubism" },
                new Genre { Description = "Renaissance" },
                new Genre { Description = "Baroque" },
                new Genre { Description = "Pop Art" }
            };
            // Loop through each genre and add to database
            foreach (var genre in genres) 
                repository.AddGenre(genre);

            // 3. SEED EXHIBITIONS - Add exhibition events to the Exhibition table
            Console.WriteLine("Adding exhibitions...");
            var exhibitions = new[]
            {
                new Exhibition { Description = "Modern Art Masterpieces" },
                new Exhibition { Description = "Renaissance Revival" },
                new Exhibition { Description = "Surrealism Exhibition" },
                new Exhibition { Description = "Impressionist Showcase" },
                new Exhibition { Description = "International Art Fair" },
                new Exhibition { Description = "Baroque Art Exhibition" },
                new Exhibition { Description = "Contemporary Art Show" }
            };
            // Loop through each exhibition and add to database
            foreach (var exhibition in exhibitions) 
                repository.AddExhibition(exhibition);

            // 4. SEED ARTWORKS - Add artwork pieces to the Artwork table
            // that were auto-generated when we inserted the records above
            Console.WriteLine("Adding artworks...");
            var artworks = new[]
            {
                // GenreID: 1=Abstract, 2=Impressionism, 3=Surrealism, 4=Cubism, 5=Renaissance, 6=Baroque, 7=Pop Art
                // ArtistID: 1=Michelangelo, 2=van Gogh, 3=Monet, 4=Picasso, 5=Kahlo, 6=da Vinci, 7=van Rijn, 8=Dali
                
                new Artwork { Title = "Starry Night", GenreID = 2, ArtistID = 2 },
                new Artwork { Title = "Sunflowers", GenreID = 2, ArtistID = 2 },
                new Artwork { Title = "Guernica", GenreID = 4, ArtistID = 4 },
                new Artwork { Title = "The Persistence of Memory", GenreID = 3, ArtistID = 8 },
                new Artwork { Title = "Water Lilies", GenreID = 2, ArtistID = 3 },
                new Artwork { Title = "Self-Portrait with Thorn Necklace", GenreID = 1, ArtistID = 5 },
                new Artwork { Title = "The Weeping Woman", GenreID = 4, ArtistID = 4 },
                new Artwork { Title = "The Creation of Adam", GenreID = 5, ArtistID = 1 },
                new Artwork { Title = "The Elephants", GenreID = 3, ArtistID = 8 },
                new Artwork { Title = "Impression Sunrise", GenreID = 2, ArtistID = 3 },
                new Artwork { Title = "Mona Lisa", GenreID = 5, ArtistID = 6 },
                new Artwork { Title = "The Night Watch", GenreID = 6, ArtistID = 7 },
                new Artwork { Title = "Campbell's Soup Cans", GenreID = 7, ArtistID = 4 },
                new Artwork { Title = "Girl with a Pearl Earring", GenreID = 6, ArtistID = 7 },
                new Artwork { Title = "The Last Supper", GenreID = 5, ArtistID = 6 }
            };
            // Loop through each artwork and add to database
            foreach (var artwork in artworks) 
                repository.AddArtwork(artwork);

            // 5. SEED ENTRIES - Link artworks to exhibitions 
            // Each entry connects an ArtworkID to an ExhibitionID
            Console.WriteLine("Adding entries...");
            var entries = new[]
            {
                // ArtworkID: 1-15 correspond to the artworks added above
                // ExhibitionID: 1=Modern Art, 2=Renaissance, 3=Surrealism, 4=Impressionist, 5=International, 6=Baroque, 7=Contemporary
                
                // Impressionist Showcase (Exhibition 4)
                new Entry { ArtworkID = 1, ExhibitionID = 4 }, // Starry Night
                new Entry { ArtworkID = 2, ExhibitionID = 4 }, // Sunflowers
                new Entry { ArtworkID = 5, ExhibitionID = 4 }, // Water Lilies
                new Entry { ArtworkID = 10, ExhibitionID = 4 }, // Impression Sunrise
                new Entry { ArtworkID = 8, ExhibitionID = 4 }, // The Creation of Adam
                
                // Modern Art Masterpieces (Exhibition 1)
                new Entry { ArtworkID = 3, ExhibitionID = 1 }, // Guernica
                new Entry { ArtworkID = 7, ExhibitionID = 1 }, // The Weeping Woman
                new Entry { ArtworkID = 1, ExhibitionID = 1 }, // Starry Night
                new Entry { ArtworkID = 5, ExhibitionID = 1 }, // Water Lilies
                
                // Surrealism Exhibition (Exhibition 3)
                new Entry { ArtworkID = 4, ExhibitionID = 3 }, // The Persistence of Memory
                new Entry { ArtworkID = 9, ExhibitionID = 3 }, // The Elephants
                
                // Renaissance Revival (Exhibition 2)
                new Entry { ArtworkID = 6, ExhibitionID = 2 }, // Self-Portrait
                new Entry { ArtworkID = 8, ExhibitionID = 2 }, // The Creation of Adam
                new Entry { ArtworkID = 11, ExhibitionID = 2 }, // Mona Lisa
                new Entry { ArtworkID = 15, ExhibitionID = 2 }, // The Last Supper
                
                // International Art Fair (Exhibition 5)
                new Entry { ArtworkID = 3, ExhibitionID = 5 }, // Guernica
                new Entry { ArtworkID = 7, ExhibitionID = 5 }, // The Weeping Woman
                
                // Baroque Art Exhibition (Exhibition 6)
                new Entry { ArtworkID = 12, ExhibitionID = 6 }, // The Night Watch
                new Entry { ArtworkID = 14, ExhibitionID = 6 }, // Girl with a Pearl Earring
                
                // Contemporary Art Show (Exhibition 7)
                new Entry { ArtworkID = 13, ExhibitionID = 7 } // Campbell's Soup Cans
            };
            // Loop through each entry and add to database
            foreach (var entry in entries) 
                repository.AddEntry(entry);

            // 6. CONFIRMATION - Let the user know seeding is complete
            Console.WriteLine(" Database seeded successfully!");
            Console.WriteLine($"   - {artists.Length} artists added");
            Console.WriteLine($"   - {genres.Length} genres added");
            Console.WriteLine($"   - {exhibitions.Length} exhibitions added");
            Console.WriteLine($"   - {artworks.Length} artworks added");
            Console.WriteLine($"   - {entries.Length} entries added");
        }
    }
}