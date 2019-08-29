using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IKiwiRepository _repo;
private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
         public UsersController(IKiwiRepository repo,IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _repo = repo;
            _httpContextAccessor= httpContextAccessor;
        }
         [AllowAnonymous]
         public string Get()
          {
            return "Mohsin";
          }
        [HttpGet]
         public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            var usersToReturn =_mapper.Map<IEnumerable<USerListDTO>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn =_mapper.Map<UserDetailDTO>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,[FromBody]UserUpdateDTO userUpdateDto)
        {
          
           //var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
           if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

                 var userFromRepo =await _repo.GetUser(id);

                 _mapper.Map(userUpdateDto, userFromRepo);
                 if(await _repo.SaveAll())
                    return NoContent();

                    throw new Exception($"Updating user {id} failed on save");
        } 
    }
}