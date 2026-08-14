using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryManagement.Models
{
    /// Artwork model - represents a piece of art
    public class Artwork
    {
        // Unique identifier for the artwork to match ArtworkID
        public int ArtworkID { get; set; }

        // Title of the artwork 
        public string Title { get; set; } = string.Empty;

        // Foreign key reference to Genre table
        public int GenreID { get; set; }

        // Foreign key reference to Artist table
        public int ArtistID { get; set; }

        // Navigation properties for Entity Framework 
        public virtual Genre? Genre { get; set; }
        public virtual Artist? Artist { get; set; }

        /// Override ToString to return the artwork title
        public override string ToString()
        {
            return Title;
        }
    }
}