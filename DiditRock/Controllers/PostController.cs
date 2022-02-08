using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DiditRock.Repositories;
using DiditRock.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DiditRock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        public PostController(IPostRepository postRepository, IUserProfileRepository userProfileRepository)
        {
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var posts = _postRepository.GetAll();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var post = _postRepository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }

        [HttpGet("myPosts")]
        public IActionResult GetLoggedInUserPosts()
        {
            var loggedInUser = GetCurrentUserProfile();
            var posts = _postRepository.GetAllPostsForUser(loggedInUser.Id);
            return Ok(posts);
        }

        [HttpPost]
        public IActionResult Post(Post post)
        { 
                
                _postRepository.Add(post);
                return CreatedAtAction("Get", new { id = post.Id }, post);

            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _postRepository.Delete(id);
            return NoContent();
        }
        [HttpPut]
        public IActionResult Update(Post post)
        {
            try
            {
                _postRepository.Update(post);

                return Ok(post);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

