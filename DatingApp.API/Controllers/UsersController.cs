using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
  // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IKiwiRepository _repo;

        private readonly IMapper _mapper;
         public UsersController(IKiwiRepository repo,IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
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
        public async Task<IActionResult> UpdateUser(int id,UserUpdateDTO userUpdateDto)
        {
          // if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               // return Unauthorized();

                 var userFromRepo =await _repo.GetUser(id);

                 _mapper.Map(userUpdateDto, userFromRepo);
                 if(await _repo.SaveAll())
                    return NoContent();

                    throw new Exception($"Updating user {id} failed on save");
        } 
    }
}