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

namespace API.Controllers
{

    [Authorize]
    public class UsersController:BaseApiController
    {

        private readonly DataContext _context;
        private readonly IUserRepository _repository;
        public UsersController(DataContext context, IUserRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            
           // var usr = await _context.Users.ToListAsync();
           
            //return Ok(usr.Select(x=>x.UserName).Where(x=>x=="p"));//
           var users = await _repository.GetUsersAsync();
            return Ok(users);//await _context.Users.ToListAsync();
        }
        
     /*   [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUsersById(int id)
        {
          // return _context.Users.FindAsync(id);
          // await _context.Users.Include(p=>p.Photos).Where(x=>x.Id==id).FirstOrDefaultAsync(x=>x.Id==id);
          return await _repository.GetUserByIdAsync(id);
        }
        */

                
        [HttpGet("{username}")]
        public async Task<ActionResult<AppUser>> GetUsersByUsername(string username)
        {
          // return _context.Users.FindAsync(id);
        // await _context.Users.Include(p=>p.Photos).Where(x=>x.Id==id).FirstOrDefaultAsync(x=>x.Id==id);
            return await _repository.GetUserByUsernameAsync(username);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AppUser>> DeleteCommand(int id)
        {
            var UserModel = _context.Users.FindAsync(id);
           
            //_context.Remove(UserModel);

            return await UserModel;
        }
        

    }
}