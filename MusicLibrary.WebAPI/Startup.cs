using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicLibrary.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicLibrary.Infrastructure.Services;
using MusicLibrary.Infrastructure.Interfaces;
using MusicLibrary.Infrastructure.Models;
using System.Text;
using MusicLibrary.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using MusicLibrary.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MusicLibrary.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");  //Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext(connectionString);
            services.AddControllers();

            // Identity Server
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MusicLibraryDbContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<ISongService, SongService>();

            //Configure JWT
            var appSettingSection = Configuration.GetSection("JWT");
            services.Configure<AppSettings>(appSettingSection);

            var appSettings = appSettingSection.Get<AppSettings>();
            var secretKey = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding JWT Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Music Library API",
                    Version = "v1",
                    Description = "music library by album and genres.",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Music Library Web API"));
        }
    }
}
