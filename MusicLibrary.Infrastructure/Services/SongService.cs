using MusicLibrary.Infrastructure.Entities;
using MusicLibrary.Infrastructure.Interfaces;
using MusicLibrary.Infrastructure.Models.Songs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Infrastructure.Services
{
    public class SongService : ISongService
    {
        public Task<Songs> DeleteSong(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SongCreateModel> GetSongById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SongViewModel>> GetSongs()
        {
            throw new NotImplementedException();
        }

        public Task SaveSong(SongCreateModel model)
        {
            throw new NotImplementedException();
        }
    }
}
