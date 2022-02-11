using System;
using Microsoft.AspNetCore.Mvc;
using DiditRock.Models;
using DiditRock.Repositories;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace DiditRock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ConcertController : ControllerBase
    {
        private readonly IConcertRepository _concertRepository;
        private readonly IConcertArtistRepository _concertArtistRepository;
        public ConcertController(IConcertRepository concertRepository, IConcertArtistRepository concertArtistRepository)
        {
            _concertRepository = concertRepository;
            _concertArtistRepository = concertArtistRepository;

        }

        [HttpGet]
        public IActionResult Get()
        {
            var concerts = _concertRepository.GetAll();

            return Ok(concerts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var concert = _concertRepository.GetById(id);
            if (concert == null)
            {
                return NotFound();
            }
            return Ok(concert);
        }

        [HttpPost]
        public IActionResult Post(Concert concert)
        {
            _concertRepository.Add(concert);
            _concertArtistRepository.Add(new ConcertArtist{ ConcertId=concert.Id, ArtistId=(int)concert.ArtistId});
            return CreatedAtAction("Get", new { id = concert.Id }, concert);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Concert concert)
        {
            if (id != concert.Id)
            {
                return BadRequest();
            }

            _concertRepository.Update(concert);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _concertRepository.Delete(id);
            return NoContent();
        }
    }
}