using DiditRock.Models;
using DiditRock.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiditRock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertArtistController : ControllerBase
    {
        private readonly IConcertRepository _concertRepository;
        private readonly IConcertArtistRepository _concertArtistRepository;
        private readonly IUserProfileRepository _userProfileRepository;


        public ConcertArtistController(IConcertRepository concertRepository, IConcertArtistRepository concertArtistRepository, IUserProfileRepository userProfileRepository)
        {
            _concertRepository = concertRepository;
            _concertArtistRepository = concertArtistRepository;
            _userProfileRepository = userProfileRepository;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var concertArtist = _concertArtistRepository.GetById(id);
            return Ok(concertArtist);
        }

        [HttpGet("GetConcertArtists/{id}")]
        public IActionResult GetByConcertId(int id)
        {
            var concertArtists = _concertArtistRepository.GetConcertArtistsByConcertId(id);
            return Ok(concertArtists);
        }

        // POST api/<ConcertArtistController>
        [HttpPost]
        public IActionResult Post(ConcertArtist concertArtist)
        {
            _concertArtistRepository.Add(concertArtist);
            return NoContent();
        }

        // DELETE api/<ConcertArtistController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _concertArtistRepository.Delete(id);
            return NoContent();
        }
    }
}