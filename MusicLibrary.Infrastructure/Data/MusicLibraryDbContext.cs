using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicLibrary.Infrastructure.Data
{
    public class MusicLibraryDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Albums> Albums { get; set; }
        public DbSet<AlbumComposers> AlbumComposers { get; set; }
        public DbSet<Songs> Songs { get; set; }
        public DbSet<SongsDetail> SongsDetail { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Genre> Genre { get; set; }

        public MusicLibraryDbContext(DbContextOptions<MusicLibraryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
