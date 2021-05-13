using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicLibrary.Infrastructure.Entities
{
    public class Albums
    {
        [Key]
        public int AlbumId { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }
        public string AlbumThumb { get; set; }
        public int GenreId { get; set; }
        //public string SingerIds { get; set; } // separated by comma
    }
}
