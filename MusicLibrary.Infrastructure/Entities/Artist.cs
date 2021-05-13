using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicLibrary.Infrastructure.Entities
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        
        [MaxLength(50)]
        public string ArtistName { get; set; }
        public string ArtistInfo { get; set; } // i.e. a artist can be singer,lyricist or composer 
        public DateTime DOB { get; set; }

    }
}
