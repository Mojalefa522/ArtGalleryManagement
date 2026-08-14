using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryManagement.Models
{
    public class Artist
    {
        // Unique identifier for the artist
        public int ArtistID { get; set; }

        // Artist's first name
        public string Name { get; set; } = string.Empty;

        // Artist's surname
        public string Surname { get; set; } = string.Empty;

        public string FullName => $"{Name} {Surname}".Trim();

        public override string ToString()
        {
            return FullName;
        }
    }
}