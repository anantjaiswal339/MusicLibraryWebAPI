using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicLibrary.Infrastructure.Entities
{
    public class SongsDetail
    {
        [Key]
        public Int64 SongDetailId { get; set; }
        public Int64 SongId { get; set; }
        public int ArtistId { get; set; }
        public string ArtistType { get; set; } // i.e. Singer/Composer/Lyricist
    }
}
