using MusicLibrary.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Infrastructure.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetArtists();
        Task<Artist> GetArtistById(int id);
        Task SaveArtist(Artist album);
        Task UpdateArtist(int id, Artist album);
        Task<Artist> DeleteArtist(int id);
    }
}
