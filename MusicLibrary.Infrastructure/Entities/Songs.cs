using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicLibrary.Infrastructure.Entities
{
    public class Songs
    {
        [Key]
        public Int64 SongId { get; set; }
        public int AlbumId { get; set; }
        public string SongName { get; set; }
        public string SongThumb { get; set; }
        public float Duration { get; set; }        
    }
}
