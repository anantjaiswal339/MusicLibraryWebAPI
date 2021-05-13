using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Infrastructure.Data;
using MusicLibrary.Infrastructure.Entities;
using MusicLibrary.Infrastructure.Interfaces;
using MusicLibrary.Infrastructure.Models;
using MusicLibrary.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicLibrary.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public async Task<IEnumerable<Albums>> Get()
        {
            return await _albumService.GetAlbums();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var album = await _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            return Ok(album);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Albums album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _albumService.SaveAlbum(album);
            return CreatedAtAction("Get", new { id = album.AlbumId }, album);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Albums model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var album = await _albumService.GetAlbumById(id);
            //if (album == null)
            //{
            //    return NotFound();
            //}
            await _albumService.UpdateAlbum(id, model);
            return CreatedAtAction("Get", new { id = model.AlbumId }, model);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var album = await _albumService.DeleteAlbum(id);
                if (album == null)
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
