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

        public MusicLibraryDbContext(DbContextOptions<MusicLibraryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
