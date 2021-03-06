using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _service;
        public AccountController(DataContext context, ITokenService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegiserDto registerDto)
        {
            using var hmac = new HMACSHA512();
           if(await UserExists(registerDto.Username)) return BadRequest("Username is already taken");
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Username = user.UserName,
                Token = _service.CreateToken(user)
            };
        }

        
         [HttpPost("login")]
         public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
         {
             var user = await _context.Users.
                                SingleOrDefaultAsync(x=>x.UserName == loginDto.Username);
            if(user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i=0; i<computedHash.Length; i++)
            {
                if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _service.CreateToken(user)
            };
             
         }
         [HttpGet("check")]
         public async Task<ActionResult> jsonCheckData()
         {
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
           
             var data = Seed.SeedUsers(_context).ToString();
                 var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
                 foreach(var user in users)
                 {
                     using var hmac = new HMACSHA512();
                     user.UserName = user.UserName.ToLower();
                     user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                     user.PasswordSalt = hmac.Key;
                     return Ok(user);
                 }
          

             return Ok(users);
         }

        private async Task<bool> UserExists(string username )
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}