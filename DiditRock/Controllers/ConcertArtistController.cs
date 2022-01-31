using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiditRock.Models;
using DiditRock.Repositories;

namespace DiditRock.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertArtistController : Controller
    {
        private readonly IConcertArtistRepository _concertArtistRepo;
        private readonly IConcertRepository _concertRepo;

        public ConcertArtistController(IConcertArtistRepository concertArtistRepo, IConcertRepository concertRepo)
        {
            _concertArtistRepo = concertArtistRepo;
            _concertRepo = concertRepo;
        }

        [HttpGet("Manage-Artists/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_concertArtistRepo.GetAllConcertArtistsForConcert(id));
        }

        [HttpPost("Add")]
        public IActionResult Concert(ConcertArtist concertArtist)
        {
            _concertArtistRepo.Add(concertArtist);
            return CreatedAtAction("Details", new { id = concertArtist.Id }, concertArtist);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _concertArtistRepo.Delete(id);
            return NoContent();
        }

        [HttpGet("GetById/{concertArtistId}")]
        public IActionResult GetById(int concertArtistId)
        {
            var concertArtist = _concertArtistRepo.GetConcertArtistById(concertArtistId);
            if (concertArtist == null)
            {
                return NotFound();
            }
            return Ok(concertArtist);
        }


    }
}