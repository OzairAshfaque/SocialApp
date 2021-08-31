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

namespace API.Controllers
{

    [Authorize]
    public class UsersController:BaseApiController
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository repository, IMapper mapper)
        {
       
            _repository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            
           var users = await _repository.GetUsersAsync();
           var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
          

            return Ok(usersToReturn);
        }
        


                
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUsersByUsername(string username)
        {
          // return _context.Users.FindAsync(id);
        // await _context.Users.Include(p=>p.Photos).Where(x=>x.Id==id).FirstOrDefaultAsync(x=>x.Id==id);
            //var user = await _repository.GetUserByUsernameAsync(username);
            var user = await _repository.GetMemberAsync(username);
            return user;
        }

      

    }
}