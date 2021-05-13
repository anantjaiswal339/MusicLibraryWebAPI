using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicLibrary.Infrastructure.Data;

namespace MusicLibrary.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<MusicLibraryDbContext>(options =>
                options.UseSqlite(connectionString)); // will be created in web project root
    }
}
