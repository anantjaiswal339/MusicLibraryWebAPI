using System;
using System.Collections.Generic;
using System.Text;

namespace MusicLibrary.Infrastructure.Models.Songs
{
    public class SongDetailViewModel
    {
        public Int64 SongsDetailId { get; set; }        
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistType { get; set; } // i.e. Singer/Composer/Lyricist

    }
}
