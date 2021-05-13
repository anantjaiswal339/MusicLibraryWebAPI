using System;
using System.Collections.Generic;
using System.Text;

namespace MusicLibrary.Infrastructure.Models.Songs
{
    public class SongCreateModel
    {
        public Int64 SongId { get; set; }
        public int AlbumId { get; set; }
        public string SongName { get; set; }
        public string SongThumb { get; set; }
        public float Duration { get; set; }
        public List<SongDetailCreateModel> SongDetails { get; set; }
    }
}
