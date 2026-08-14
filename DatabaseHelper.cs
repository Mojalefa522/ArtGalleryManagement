using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ArtGalleryManagement
{
    /// DatabaseHelper class handles database connection and table creation
    public class DatabaseHelper
    {
        // Connection string for SQLite database
        private readonly string _connectionString;

        /// Constructor - initializes database connection and creates tables if they don't exist
        /// <param name="connectionString">SQLite connection string (default: Data Source=artgallery.db)</param>
        public DatabaseHelper(string connectionString = "Data Source=artgallery.db")
        {
            _connectionString = connectionString;
            InitializeDatabase(); // Create tables when the helper is instantiated
        }

        /// Creates all necessary database tables if they don't already exist
        private void InitializeDatabase()
        {
            // Open a connection to the SQLite database
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            // Execute SQL commands to create all tables
            using var cmd = new SqliteCommand(@"
                -- Artist table: Stores artist information
                CREATE TABLE IF NOT EXISTS Artist (
                    ArtistID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Surname TEXT NOT NULL
                );

                -- Genre table: Stores art genre categories
                CREATE TABLE IF NOT EXISTS Genre (
                    GenreID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Description TEXT NOT NULL
                );

                -- Exhibition table: Stores exhibition information
                CREATE TABLE IF NOT EXISTS Exhibition (
                    ExhibitionID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Description TEXT NOT NULL
                );

                -- Artwork table: Stores artwork details with foreign keys to Artist and Genre
                CREATE TABLE IF NOT EXISTS Artwork (
                    ArtworkID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    GenreID INTEGER NOT NULL,
                    ArtistID INTEGER NOT NULL,
                    FOREIGN KEY (GenreID) REFERENCES Genre(GenreID),
                    FOREIGN KEY (ArtistID) REFERENCES Artist(ArtistID)
                );

                -- Entry table: Links artworks to exhibitions (many-to-many relationship)
                CREATE TABLE IF NOT EXISTS Entry (
                    EntryID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ArtworkID INTEGER NOT NULL,
                    ExhibitionID INTEGER NOT NULL,
                    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID),
                    FOREIGN KEY (ExhibitionID) REFERENCES Exhibition(ExhibitionID)
                );
            ", connection);
            cmd.ExecuteNonQuery(); // Execute the SQL commands
        }

        /// Returns a new SQLite connection to the database
        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString);
        }
    }
}