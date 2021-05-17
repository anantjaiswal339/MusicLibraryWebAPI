using Microsoft.EntityFrameworkCore;
using MusicLibrary.Infrastructure.Data;
using MusicLibrary.Infrastructure.Entities;
using MusicLibrary.Infrastructure.Interfaces;
using MusicLibrary.Infrastructure.Models.Albums;
using MusicLibrary.Infrastructure.Models.Songs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Infrastructure.Services
{
    public class SongService : ISongService
    {
        private readonly MusicLibraryDbContext _context;
        public SongService(MusicLibraryDbContext context)
        {
            _context = context;
        }
        public async Task<SongCreateModel> GetSongById(Int64 id)
        {
            SongCreateModel songViewModel = null;
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                var songDetail = _context.SongsDetail.Where(x => x.SongId == id).Select(x => new SongDetailCreateModel
                {
                    ArtistId = x.ArtistId,
                    ArtistType = x.ArtistType,
                    SongId = x.SongId,
                    SongsDetailId = x.SongDetailId
                }).ToList();

                songViewModel = new SongCreateModel
                {
                    AlbumId = song.AlbumId,
                    Duration = song.Duration,
                    SongId = song.SongId,
                    SongName = song.SongName,
                    SongThumb = song.SongThumb,
                    SongDetails = songDetail
                };
            }

            return songViewModel;
        }

        public async Task<IEnumerable<SongViewModel>> GetSongs()
        {
            var querySongArtist = _context.SongsDetail.Join(_context.Artist, r => r.ArtistId, p => p.ArtistId, (r, p) => new { r.SongDetailId, r.ArtistId, r.SongId, r.ArtistType, p.ArtistName, p.ArtistInfo });

            var songs = _context.Songs.Select(x => new SongViewModel
            {
                SongId = x.SongId,
                Duration = x.Duration,
                SongName = x.SongName,
                SongThumb = x.SongThumb,
                Album = _context.Albums.Where(y => y.AlbumId == x.AlbumId).Select(y => new AlbumViewModel
                {
                    Title = y.Title,
                    AlbumId = y.AlbumId,
                    AlbumThumb = y.AlbumThumb
                }).FirstOrDefault(),
                SongDetails = querySongArtist.Where(y => y.SongId == x.SongId).Select(y => new SongDetailViewModel
                {
                    SongsDetailId = y.SongDetailId,
                    ArtistId = y.ArtistId,
                    ArtistName = y.ArtistName,
                    ArtistType = y.ArtistType,
                }).ToList()

            });
            return songs;
        }

        public async Task<SongCreateModel> SaveSong(SongCreateModel model)
        {
            Songs song = new Songs
            {
                AlbumId = model.AlbumId,
                Duration = model.Duration,
                SongName = model.SongName,
                SongThumb = model.SongThumb
            };

            _context.Add(song);
            _context.SaveChanges();

            var songsDetail = model.SongDetails.Select(x => new SongsDetail
            {
                ArtistId = x.ArtistId,
                ArtistType = x.ArtistType,
                SongId = song.SongId
            });

            _context.AddRange(songsDetail);
            await _context.SaveChangesAsync();

            model.SongId = song.SongId;
            return model;
        }

        public async Task<Songs> DeleteSong(Int64 id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
                var songDetail = _context.SongsDetail.Where(x => x.SongId == id);
                _context.SongsDetail.RemoveRange(songDetail);
            }
            return song;
        }

    }
}
