using Microsoft.AspNetCore.Mvc;
using System;
using DiditRock.Models;
using DiditRock.Repositories;

namespace DiditRock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult Get()
        {
            return Ok(_userProfileRepository.GetAllUsers());
        }

        [HttpGet("{firebaseUserId}")]
        public IActionResult GetUserProfile(string firebaseUserId)
        {
            return Ok(_userProfileRepository.GetByFirebaseUserId(firebaseUserId));
        }

        [HttpGet("GetUserProfileById/{id}")]
        public IActionResult GetUserProfileById(int id)
        {
            return Ok(_userProfileRepository.GetUserProfileById(id));
        }

        [HttpGet("DoesUserExist/{firebaseUserId}")]
        public IActionResult DoesUserExist(string firebaseUserId)
        {
            var userProfile = _userProfileRepository.GetByFirebaseUserId(firebaseUserId);

            return Ok();
        }

        [HttpPost]
        public IActionResult Post(UserProfile userProfile)
        {
            userProfile.CreateDateTime = DateTime.Now;
            userProfile.UserTypeId = UserType.AUTHOR_ID;
            _userProfileRepository.Add(userProfile);
            return CreatedAtAction(
                nameof(GetUserProfile),
                new { firebaseUserId = userProfile.FirebaseUserId },
                userProfile);
        }

        [HttpPut("UpdateUserType2/userId")]
        public ActionResult UpdateUserType2(int id)
        {
            UserProfile userProfile = _userProfileRepository.GetUserProfileId(id);

            userProfile.UserTypeId = 2;

            return Ok();


        }

        [HttpPut("UpdateUserType1/userId")]
        public ActionResult UpdateUserType1(int id)
        {
            UserProfile userProfile = _userProfileRepository.GetUserProfileId(id);

            userProfile.UserTypeId = 1;

            return Ok();


        }

        [HttpGet("GetUserTypes")]
        public IActionResult AllUserTypes()
        {
            return Ok(_userProfileRepository.AllUserTypes());
        }

    }
}