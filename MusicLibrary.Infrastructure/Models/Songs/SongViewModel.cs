using MusicLibrary.Infrastructure.Models.Albums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicLibrary.Infrastructure.Models.Songs
{
    public class SongViewModel
    {
        public Int64 SongId { get; set; }
        //public int AlbumId { get; set; }
        public string SongName { get; set; }
        public string SongThumb { get; set; }
        public float Duration { get; set; }
        public AlbumViewModel Album { get; set; }
        public List<SongDetailViewModel> SongDetails { get; set; }
    }
}
