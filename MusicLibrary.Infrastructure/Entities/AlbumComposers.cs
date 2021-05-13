using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicLibrary.Infrastructure.Entities
{
    public class AlbumComposers
    {
        [Key]
        public int AlbumComposerId { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
    }
}
