using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Infrastructure.Interfaces;
using MusicLibrary.Infrastructure.Models;
using MusicLibrary.Infrastructure.Models.Songs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibrary.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongService _songService;
        public SongsController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public async Task<IEnumerable<SongViewModel>> Get()
        {
            return await _songService.GetSongs();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Int64 id)
        {
            var song = await _songService.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(song);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SongCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var song =  await _songService.SaveSong(model);
            return CreatedAtAction("Get", new { id = song.SongId }, model);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Int64 id)
        {
            try
            {
                var song = await _songService.DeleteSong(id);
                if (song == null)
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
