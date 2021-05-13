using MusicLibrary.Infrastructure.Entities;
using MusicLibrary.Infrastructure.Models.Songs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Infrastructure.Interfaces
{
    public interface ISongService
    {
        Task<IEnumerable<SongViewModel>> GetSongs();
        Task<SongCreateModel> GetSongById(int id);
        Task SaveSong(SongCreateModel model);

        Task<Songs> DeleteSong(int id);
    }
}
