using System;
using Microsoft.AspNetCore.Mvc;
using DiditRock.Models;
using DiditRock.Repositories;

namespace DiditRock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly IVenueRepository _venueRepository;
        public VenueController(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _venueRepository.GetAll();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var venue = _venueRepository.GetById(id);
            if (venue == null)
            {
                return NotFound();
            }
            return Ok(venue);
        }

        [HttpPost]
        public IActionResult Post(Venue venue)
        {
            _venueRepository.Add(venue);
            return CreatedAtAction("Get", new { id = venue.Id }, venue);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Venue venue)
        {
            if (id != venue.Id)
            {
                return BadRequest();
            }

            _venueRepository.Update(venue);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _venueRepository.Delete(id);
            return NoContent();
        }
    }
}