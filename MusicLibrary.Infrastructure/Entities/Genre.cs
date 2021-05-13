using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicLibrary.Infrastructure.Entities
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
