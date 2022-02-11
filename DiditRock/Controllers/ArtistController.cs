using System;
using Microsoft.AspNetCore.Mvc;
using DiditRock.Models;
using DiditRock.Repositories;

namespace DiditRock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;
        public ArtistController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var artists = _artistRepository.GetAll();

            return Ok(artists);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var artist = _artistRepository.GetById(id);
            if (artist == null)
            {
                return NotFound();
            }
            return Ok(artist);
        }

        [HttpGet("GetConcertArtists/{id}")]
        public IActionResult GetByConcertId(int id)
        {
            var concertArtists = _artistRepository.GetArtistsByConcertId(id);
            return Ok(concertArtists);
        }

        [HttpPost]
        public IActionResult Post(Artist artist)
        {
            _artistRepository.Add(artist);
            return CreatedAtAction("Get", new { id = artist.Id }, artist);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Artist artist)
        {
            if (id != artist.Id)
            {
                return BadRequest();
            }

            _artistRepository.Update(artist);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _artistRepository.Delete(id);
            return NoContent();
        }
    }
}