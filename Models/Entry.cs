using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryManagement.Models
{
    /// Entry model - represents the link between artworks and exhibitions
    public class Entry
    {
        // Unique identifier for the entry it matches EntryID
        public int EntryID { get; set; }

        // Foreign key reference to Artwork table
        public int ArtworkID { get; set; }

        // Foreign key reference to Exhibition table
        public int ExhibitionID { get; set; }

        // Navigation properties for Entity Framework 
        public virtual Artwork? Artwork { get; set; }
        public virtual Exhibition? Exhibition { get; set; }
    }
}