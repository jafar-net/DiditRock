using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiditRock.Repositories;
using DiditRock.Models;


namespace DiditRock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertArtistController : ControllerBase
    {
        private readonly IConcertArtistRepository _concertArtistRepository;

        public ConcertArtistController(IConcertArtistRepository concertArtistRepository)
        {
            _concertArtistRepository = concertArtistRepository;
        }

        // POST api/<ValuesController>
        [HttpGet("{concertId}")]
        public IActionResult Get(int concertId)
        {
            try
            {
                var concertArtists = _concertArtistRepository.Get(concertId);
                return Ok(concertArtists);
            }
            catch
            {
                return NoContent();
            }
        }
        [HttpPost]
        public IActionResult Update(ConcertArtist concertArtist)
        {
            var concertArtists = _concertArtistRepository.Get(concertArtist.ConcertId);
            if (concertArtists.Any(artist => artist.ArtistId == concertArtist.ArtistId))
                try
                {
                    _concertArtistRepository.Delete(concertArtist);
                    return Ok(_concertArtistRepository.Get(concertArtist.ConcertId));
                }
                catch
                {
                    return BadRequest();
                }
            else
                try
                {
                    _concertArtistRepository.Add(concertArtist);
                    return Ok(_concertArtistRepository.Get(concertArtist.ConcertId));
                }
                catch
                {
                    return BadRequest();
                }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(ConcertArtist concertArtist)
        {
            _concertArtistRepository.Delete(concertArtist);
            return NoContent();
        }
    }
}
