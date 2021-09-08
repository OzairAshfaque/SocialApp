using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Controllers;
using API.Interfaces;
using AutoMapper;
using System.Security.Claims;

namespace API.Controllers
{

    [Authorize]
    public class UsersController:BaseApiController
    {
        private readonly IUserRepository _repository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository repository, IMapper mapper, DataContext context)
        {
       
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            
             var user = await _repository.GetMembersAsync();
          

            return Ok(user);
        }
        


                
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUsersByUsername(string username)
        { 
            

            return  await _repository.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _repository.GetUserByUsernameAsync(username);

            _mapper.Map(memberUpdateDto, user);

            _repository.Update(user);

            if(await  _repository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");

        }
    }
}