using MusicLibrary.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Infrastructure.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<Albums>> GetAlbums();
        Task<Albums> GetAlbumById(int id);
        Task SaveAlbum(Albums album);
        Task UpdateAlbum(int id, Albums album);

        Task<Albums> DeleteAlbum(int id);
    }
}
