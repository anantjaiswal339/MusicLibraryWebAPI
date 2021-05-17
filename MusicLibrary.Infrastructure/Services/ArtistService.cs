using Microsoft.EntityFrameworkCore;
using MusicLibrary.Infrastructure.Data;
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
        private readonly MusicLibraryDbContext _context;
        public ArtistService(MusicLibraryDbContext context)
        {
            _context = context;
        }
        public async Task<Artist> DeleteArtist(int id)
        {
            var artist = await _context.Artist.FindAsync(id);
            if (artist != null)
                _context.Artist.Remove(artist);

            return artist;
        }

        public async Task<Artist> GetArtistById(int id)
        {
            return await _context.Artist.FindAsync(id);
        }

        public async Task<IEnumerable<Artist>> GetArtists()
        {
            return await _context.Artist.ToListAsync();
        }

        public async Task SaveArtist(Artist artist)
        {
            _context.Add(artist);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateArtist(int id, Artist artist)
        {
            _context.Entry(artist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
