using System;
using System.Collections.Generic;
using System.Text;

namespace MusicLibrary.Infrastructure.Models
{
    internal class AlbumCreateViewModel
    {
        public Guid AlbumId { get; set; }
        public string Title { get; set; }
        public string Composers { get; set; }
    }
}
