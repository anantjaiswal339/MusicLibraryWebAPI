using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Infrastructure.Entities;
using MusicLibrary.Infrastructure.Interfaces;
using MusicLibrary.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibrary.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<IEnumerable<Artist>> Get()
        {
            return await _artistService.GetArtists();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }
            return Ok(artist);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _artistService.SaveArtist(artist);
            return CreatedAtAction("Get", new { id = artist.ArtistId }, artist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Artist model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _artistService.UpdateArtist(id, model);
            return CreatedAtAction("Get", new { id = model.ArtistId }, model);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var artist = await _artistService.DeleteArtist(id);
                if (artist == null)
                    return NotFound();

                return Ok("Deleted Successfully!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete record!");
            }
        }
    }
}
