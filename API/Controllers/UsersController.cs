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

namespace API.Controllers
{

    [Authorize]
    public class UsersController:BaseApiController
    {

        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            
           // var usr = await _context.Users.ToListAsync();
           
            //return Ok(usr.Select(x=>x.UserName).Where(x=>x=="p"));//
            return await _context.Users.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUsersById(int id)
        {
          // return _context.Users.FindAsync(id);
            return await _context.Users.FirstOrDefaultAsync(x=>x.Id==id);
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