using MusicLibrary.Infrastructure.Entities;
using MusicLibrary.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Infrastructure.Services
{
    public class ArtistService : IArtistService
    {
        public Task<Artist> DeleteArtist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Albums> GetArtistById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Artist>> GetArtists()
        {
            throw new NotImplementedException();
        }

        public Task SaveArtist(Artist album)
        {
            throw new NotImplementedException();
        }

        public Task<Artist> UpdateArtist(int id, Artist album)
        {
            throw new NotImplementedException();
        }
    }
}
