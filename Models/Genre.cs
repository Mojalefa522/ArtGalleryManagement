using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryManagement.Models
{
    public class Genre
    {
        // Unique identifier for the genre to match GenreID
        public int GenreID { get; set; }

        // Genre name or description 
        public string Description { get; set; } = string.Empty;

        /// Override ToString to return the genre description
        public override string ToString()
        {
            return Description;
        }
    }
}