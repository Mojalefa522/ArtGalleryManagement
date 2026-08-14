using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryManagement.Models
{
    /// Exhibition model - represents an art exhibition or event
    public class Exhibition
    {
        // Unique identifier for the exhibition 
        public int ExhibitionID { get; set; }

        // Exhibition name or description 
        public string Description { get; set; } = string.Empty;

        /// Override ToString to return the exhibition description
        public override string ToString()
        {
            return Description;
        }
    }
}