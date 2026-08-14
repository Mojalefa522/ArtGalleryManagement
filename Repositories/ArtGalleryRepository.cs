using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGalleryManagement.Models;
using Microsoft.Data.Sqlite;

namespace ArtGalleryManagement.Repositories
{
    /// Repository class that implements all database operations for the Art Gallery
    /// This class handles all CRUD operations and complex queries
    public class ArtGalleryRepository : IArtGalleryRepository
    {
        // Database helper instance for managing connections
        private readonly DatabaseHelper _dbHelper;

        /// Constructor - receives a DatabaseHelper instance
        /// <param name="dbHelper">DatabaseHelper instance for database operations</param>
        public ArtGalleryRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        #region Artist Operations

        /// Retrieves all artists from the database
        public List<Artist> GetAllArtists()
        {
            var artists = new List<Artist>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand("SELECT ArtistID, Name, Surname FROM Artist", connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                artists.Add(new Artist
                {
                    ArtistID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2)
                });
            }
            return artists;
        }

        /// <summary>
        /// Adds a new artist to the database
        /// </summary>
        public void AddArtist(Artist artist)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                "INSERT INTO Artist (Name, Surname) VALUES (@Name, @Surname)",
                connection);
            cmd.Parameters.AddWithValue("@Name", artist.Name);
            cmd.Parameters.AddWithValue("@Surname", artist.Surname);
            cmd.ExecuteNonQuery();
        }

        /// Updates an existing artist's information
        public void UpdateArtist(Artist artist)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                "UPDATE Artist SET Name = @Name, Surname = @Surname WHERE ArtistID = @ArtistID",
                connection);
            cmd.Parameters.AddWithValue("@ArtistID", artist.ArtistID);
            cmd.Parameters.AddWithValue("@Name", artist.Name);
            cmd.Parameters.AddWithValue("@Surname", artist.Surname);
            cmd.ExecuteNonQuery();
        }
        /// Deletes an artist and all related data 
        public void DeleteArtist(int artistId)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            // Step 1: Delete entries related to this artist's artworks
            using var cmd1 = new SqliteCommand(@"
                DELETE FROM Entry 
                WHERE ArtworkID IN (SELECT ArtworkID FROM Artwork WHERE ArtistID = @ArtistID)",
                connection);
            cmd1.Parameters.AddWithValue("@ArtistID", artistId);
            cmd1.ExecuteNonQuery();

            // Step 2: Delete all artworks by this artist
            using var cmd2 = new SqliteCommand(
                "DELETE FROM Artwork WHERE ArtistID = @ArtistID",
                connection);
            cmd2.Parameters.AddWithValue("@ArtistID", artistId);
            cmd2.ExecuteNonQuery();

            // Step 3: Delete the artist
            using var cmd3 = new SqliteCommand(
                "DELETE FROM Artist WHERE ArtistID = @ArtistID",
                connection);
            cmd3.Parameters.AddWithValue("@ArtistID", artistId);
            cmd3.ExecuteNonQuery();
        }

        #endregion

        #region Genre Operations

        public List<Genre> GetAllGenres()
        {
            var genres = new List<Genre>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand("SELECT GenreID, Description FROM Genre", connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                genres.Add(new Genre
                {
                    GenreID = reader.GetInt32(0),
                    Description = reader.GetString(1)
                });
            }
            return genres;
        }

        public void AddGenre(Genre genre)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                "INSERT INTO Genre (Description) VALUES (@Description)",
                connection);
            cmd.Parameters.AddWithValue("@Description", genre.Description);
            cmd.ExecuteNonQuery();
        }

        public void UpdateGenre(Genre genre)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                "UPDATE Genre SET Description = @Description WHERE GenreID = @GenreID",
                connection);
            cmd.Parameters.AddWithValue("@GenreID", genre.GenreID);
            cmd.Parameters.AddWithValue("@Description", genre.Description);
            cmd.ExecuteNonQuery();
        }

        public void DeleteGenre(int genreId)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd1 = new SqliteCommand(@"
                DELETE FROM Entry 
                WHERE ArtworkID IN (SELECT ArtworkID FROM Artwork WHERE GenreID = @GenreID)",
                connection);
            cmd1.Parameters.AddWithValue("@GenreID", genreId);
            cmd1.ExecuteNonQuery();

            using var cmd2 = new SqliteCommand(
                "DELETE FROM Artwork WHERE GenreID = @GenreID",
                connection);
            cmd2.Parameters.AddWithValue("@GenreID", genreId);
            cmd2.ExecuteNonQuery();

            using var cmd3 = new SqliteCommand(
                "DELETE FROM Genre WHERE GenreID = @GenreID",
                connection);
            cmd3.Parameters.AddWithValue("@GenreID", genreId);
            cmd3.ExecuteNonQuery();
        }

        #endregion

        #region Exhibition Operations

        public List<Exhibition> GetAllExhibitions()
        {
            var exhibitions = new List<Exhibition>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand("SELECT ExhibitionID, Description FROM Exhibition", connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                exhibitions.Add(new Exhibition
                {
                    ExhibitionID = reader.GetInt32(0),
                    Description = reader.GetString(1)
                });
            }
            return exhibitions;
        }

        public void AddExhibition(Exhibition exhibition)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                "INSERT INTO Exhibition (Description) VALUES (@Description)",
                connection);
            cmd.Parameters.AddWithValue("@Description", exhibition.Description);
            cmd.ExecuteNonQuery();
        }

        public void UpdateExhibition(Exhibition exhibition)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                "UPDATE Exhibition SET Description = @Description WHERE ExhibitionID = @ExhibitionID",
                connection);
            cmd.Parameters.AddWithValue("@ExhibitionID", exhibition.ExhibitionID);
            cmd.Parameters.AddWithValue("@Description", exhibition.Description);
            cmd.ExecuteNonQuery();
        }

        public void DeleteExhibition(int exhibitionId)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd1 = new SqliteCommand(
                "DELETE FROM Entry WHERE ExhibitionID = @ExhibitionID",
                connection);
            cmd1.Parameters.AddWithValue("@ExhibitionID", exhibitionId);
            cmd1.ExecuteNonQuery();

            using var cmd2 = new SqliteCommand(
                "DELETE FROM Exhibition WHERE ExhibitionID = @ExhibitionID",
                connection);
            cmd2.Parameters.AddWithValue("@ExhibitionID", exhibitionId);
            cmd2.ExecuteNonQuery();
        }

        #endregion

        #region Artwork Operations

        public List<Artwork> GetAllArtworks()
        {
            var artworks = new List<Artwork>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                @"SELECT ArtworkID, Title, GenreID, ArtistID FROM Artwork",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                artworks.Add(new Artwork
                {
                    ArtworkID = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    GenreID = reader.GetInt32(2),
                    ArtistID = reader.GetInt32(3)
                });
            }
            return artworks;
        }

        public void AddArtwork(Artwork artwork)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                @"INSERT INTO Artwork (Title, GenreID, ArtistID) 
                  VALUES (@Title, @GenreID, @ArtistID)",
                connection);
            cmd.Parameters.AddWithValue("@Title", artwork.Title);
            cmd.Parameters.AddWithValue("@GenreID", artwork.GenreID);
            cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);
            cmd.ExecuteNonQuery();
        }

        public void UpdateArtwork(Artwork artwork)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                @"UPDATE Artwork SET Title = @Title, GenreID = @GenreID, ArtistID = @ArtistID 
                  WHERE ArtworkID = @ArtworkID",
                connection);
            cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
            cmd.Parameters.AddWithValue("@Title", artwork.Title);
            cmd.Parameters.AddWithValue("@GenreID", artwork.GenreID);
            cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);
            cmd.ExecuteNonQuery();
        }

        public void DeleteArtwork(int artworkId)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd1 = new SqliteCommand(
                "DELETE FROM Entry WHERE ArtworkID = @ArtworkID",
                connection);
            cmd1.Parameters.AddWithValue("@ArtworkID", artworkId);
            cmd1.ExecuteNonQuery();

            using var cmd2 = new SqliteCommand(
                "DELETE FROM Artwork WHERE ArtworkID = @ArtworkID",
                connection);
            cmd2.Parameters.AddWithValue("@ArtworkID", artworkId);
            cmd2.ExecuteNonQuery();
        }

        #endregion

        #region Entry Operations

        public List<Entry> GetAllEntries()
        {
            var entries = new List<Entry>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                @"SELECT EntryID, ArtworkID, ExhibitionID FROM Entry",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                entries.Add(new Entry
                {
                    EntryID = reader.GetInt32(0),
                    ArtworkID = reader.GetInt32(1),
                    ExhibitionID = reader.GetInt32(2)
                });
            }
            return entries;
        }

        public void AddEntry(Entry entry)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                @"INSERT INTO Entry (ArtworkID, ExhibitionID) 
                  VALUES (@ArtworkID, @ExhibitionID)",
                connection);
            cmd.Parameters.AddWithValue("@ArtworkID", entry.ArtworkID);
            cmd.Parameters.AddWithValue("@ExhibitionID", entry.ExhibitionID);
            cmd.ExecuteNonQuery();
        }

        public void DeleteEntry(int entryId)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(
                "DELETE FROM Entry WHERE EntryID = @EntryID",
                connection);
            cmd.Parameters.AddWithValue("@EntryID", entryId);
            cmd.ExecuteNonQuery();
        }

        #endregion

        #region Query Operations

        public List<dynamic> GetArtworksWithArtistAndGenre()
        {
            var results = new List<dynamic>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT a.Title as ArtworkTitle, 
                       ar.Name || ' ' || ar.Surname as Artist, 
                       g.Description as Genre 
                FROM Artwork a
                INNER JOIN Artist ar ON a.ArtistID = ar.ArtistID
                INNER JOIN Genre g ON a.GenreID = g.GenreID
                ORDER BY g.Description, a.Title",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dynamic item = new ExpandoObject();
                item.ArtworkTitle = reader.GetString(0);
                item.Artist = reader.GetString(1);
                item.Genre = reader.GetString(2);
                results.Add(item);
            }
            return results;
        }

        public int GetArtworkCountByGenre(string genreDescription)
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT COUNT(a.ArtworkID) 
                FROM Artwork a
                INNER JOIN Genre g ON a.GenreID = g.GenreID
                WHERE g.Description = @GenreDescription",
                connection);
            cmd.Parameters.AddWithValue("@GenreDescription", genreDescription);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public List<dynamic> GetArtistsWithArtworkCount()
        {
            var results = new List<dynamic>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT ar.Name || ' ' || ar.Surname as Artist,
                       COUNT(a.ArtworkID) as ArtworkCount
                FROM Artist ar
                LEFT JOIN Artwork a ON ar.ArtistID = a.ArtistID
                GROUP BY ar.ArtistID, ar.Name, ar.Surname
                ORDER BY ArtworkCount DESC",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dynamic item = new ExpandoObject();
                item.Artist = reader.GetString(0);
                item.ArtworkCount = reader.GetInt32(1);
                results.Add(item);
            }
            return results;
        }

        public List<dynamic> GetExhibitionsWithEntryCount()
        {
            var results = new List<dynamic>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT e.Description as Exhibition,
                       COUNT(en.EntryID) as EntryCount
                FROM Exhibition e
                LEFT JOIN Entry en ON e.ExhibitionID = en.ExhibitionID
                GROUP BY e.ExhibitionID, e.Description
                ORDER BY EntryCount DESC",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dynamic item = new ExpandoObject();
                item.Exhibition = reader.GetString(0);
                item.EntryCount = reader.GetInt32(1);
                results.Add(item);
            }
            return results;
        }

        public List<dynamic> GetGenresWithMoreThanTwoArtworks()
        {
            var results = new List<dynamic>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT g.Description as Genre,
                       COUNT(a.ArtworkID) as ArtworkCount
                FROM Genre g
                LEFT JOIN Artwork a ON g.GenreID = a.GenreID
                GROUP BY g.GenreID, g.Description
                HAVING COUNT(a.ArtworkID) > 2
                ORDER BY ArtworkCount DESC",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dynamic item = new ExpandoObject();
                item.Genre = reader.GetString(0);
                item.ArtworkCount = reader.GetInt32(1);
                results.Add(item);
            }
            return results;
        }

        public List<dynamic> GetArtistsWithMoreThanOneArtwork()
        {
            var results = new List<dynamic>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT ar.Name || ' ' || ar.Surname as Artist,
                       COUNT(a.ArtworkID) as ArtworkCount
                FROM Artist ar
                LEFT JOIN Artwork a ON ar.ArtistID = a.ArtistID
                GROUP BY ar.ArtistID, ar.Name, ar.Surname
                HAVING COUNT(a.ArtworkID) > 1
                ORDER BY ArtworkCount DESC",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dynamic item = new ExpandoObject();
                item.Artist = reader.GetString(0);
                item.ArtworkCount = reader.GetInt32(1);
                results.Add(item);
            }
            return results;
        }

        public List<dynamic> GetExhibitionsWithMoreThanThreeArtworks()
        {
            var results = new List<dynamic>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT e.Description as Exhibition,
                       COUNT(en.EntryID) as ArtworkCount
                FROM Exhibition e
                LEFT JOIN Entry en ON e.ExhibitionID = en.ExhibitionID
                GROUP BY e.ExhibitionID, e.Description
                HAVING COUNT(en.EntryID) > 3
                ORDER BY ArtworkCount DESC",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dynamic item = new ExpandoObject();
                item.Exhibition = reader.GetString(0);
                item.ArtworkCount = reader.GetInt32(1);
                results.Add(item);
            }
            return results;
        }

        public List<dynamic> GetArtworksInExhibitions()
        {
            var results = new List<dynamic>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT a.Title as ArtworkTitle,
                       ar.Name || ' ' || ar.Surname as Artist,
                       g.Description as Genre,
                       e.Description as Exhibition
                FROM Artwork a
                INNER JOIN Artist ar ON a.ArtistID = ar.ArtistID
                INNER JOIN Genre g ON a.GenreID = g.GenreID
                INNER JOIN Entry en ON a.ArtworkID = en.ArtworkID
                INNER JOIN Exhibition e ON en.ExhibitionID = e.ExhibitionID
                ORDER BY a.Title",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dynamic item = new ExpandoObject();
                item.ArtworkTitle = reader.GetString(0);
                item.Artist = reader.GetString(1);
                item.Genre = reader.GetString(2);
                item.Exhibition = reader.GetString(3);
                results.Add(item);
            }
            return results;
        }

        public List<dynamic> GetArtworksNotInExhibition()
        {
            var results = new List<dynamic>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT a.Title as ArtworkTitle,
                       ar.Name || ' ' || ar.Surname as Artist,
                       g.Description as Genre,
                       COALESCE(e.Description, 'Not in any exhibition') as Exhibition
                FROM Artwork a
                INNER JOIN Artist ar ON a.ArtistID = ar.ArtistID
                INNER JOIN Genre g ON a.GenreID = g.GenreID
                LEFT JOIN Entry en ON a.ArtworkID = en.ArtworkID
                LEFT JOIN Exhibition e ON en.ExhibitionID = e.ExhibitionID
                WHERE en.EntryID IS NULL
                ORDER BY a.Title",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dynamic item = new ExpandoObject();
                item.ArtworkTitle = reader.GetString(0);
                item.Artist = reader.GetString(1);
                item.Genre = reader.GetString(2);
                item.Exhibition = reader.GetString(3);
                results.Add(item);
            }
            return results;
        }

        public List<dynamic> GetExhibitionsAndTheirArtworks()
        {
            var results = new List<dynamic>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT e.Description as Exhibition,
                       a.Title as ArtworkTitle,
                       ar.Name || ' ' || ar.Surname as Artist
                FROM Exhibition e
                LEFT JOIN Entry en ON e.ExhibitionID = en.ExhibitionID
                LEFT JOIN Artwork a ON en.ArtworkID = a.ArtworkID
                LEFT JOIN Artist ar ON a.ArtistID = ar.ArtistID
                ORDER BY e.Description, a.Title",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dynamic item = new ExpandoObject();
                item.Exhibition = reader.GetString(0);
                item.ArtworkTitle = reader.GetString(1) ?? "No artwork";
                item.Artist = reader.GetString(2) ?? "No artist";
                results.Add(item);
            }
            return results;
        }

        public List<dynamic> GetArtistsWithArtworksAndExhibitions()
        {
            var results = new List<dynamic>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT ar.Name || ' ' || ar.Surname as Artist,
                       a.Title as Artwork,
                       g.Description as Genre,
                       e.Description as Exhibition
                FROM Artist ar
                INNER JOIN Artwork a ON ar.ArtistID = a.ArtistID
                INNER JOIN Genre g ON a.GenreID = g.GenreID
                LEFT JOIN Entry en ON a.ArtworkID = en.ArtworkID
                LEFT JOIN Exhibition e ON en.ExhibitionID = e.ExhibitionID
                ORDER BY ar.Surname, a.Title",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dynamic item = new ExpandoObject();
                item.Artist = reader.GetString(0);
                item.Artwork = reader.GetString(1);
                item.Genre = reader.GetString(2);
                item.Exhibition = reader.GetString(3) ?? "Not in any exhibition";
                results.Add(item);
            }
            return results;
        }

        public List<dynamic> GetArtistsWithTotalArtworksAndExhibitions()
        {
            var results = new List<dynamic>();
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var cmd = new SqliteCommand(@"
                SELECT ar.Name || ' ' || ar.Surname as Artist,
                       COUNT(DISTINCT a.ArtworkID) as TotalArtworks,
                       COUNT(DISTINCT en.ExhibitionID) as TotalExhibitions
                FROM Artist ar
                LEFT JOIN Artwork a ON ar.ArtistID = a.ArtistID
                LEFT JOIN Entry en ON a.ArtworkID = en.ArtworkID
                GROUP BY ar.ArtistID, ar.Name, ar.Surname
                ORDER BY TotalArtworks DESC",
                connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dynamic item = new ExpandoObject();
                item.Artist = reader.GetString(0);
                item.TotalArtworks = reader.GetInt32(1);
                item.TotalExhibitions = reader.GetInt32(2);
                results.Add(item);
            }
            return results;
        }

        #endregion
    }
}