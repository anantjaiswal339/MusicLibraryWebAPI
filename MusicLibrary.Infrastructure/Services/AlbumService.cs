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
    public class AlbumService : IAlbumService
    {
        private readonly MusicLibraryDbContext _context;
        public AlbumService(MusicLibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Albums> GetAlbumById(int id)
        {
            return await _context.Albums.FindAsync(id);
        }

        public async Task<IEnumerable<Albums>> GetAlbums()
        {
            return await _context.Albums.ToListAsync();            
        }

        public async Task SaveAlbum(Albums album)
        {
            _context.Add(album);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAlbum(int id, Albums album)
        {
            _context.Entry(album).State = EntityState.Modified;
            //_context.Albums.Update(album);
            await _context.SaveChangesAsync();
        }

        public async Task<Albums> DeleteAlbum(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
                _context.Albums.Remove(album);

            return album;
        }

    }
}
