using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                .Where(x=>x.UserName == username)
                .Select(x=>new MemberDto
                {
                    Id = x.Id,
                    Username = x.UserName,
                    PhotoUrl = x.Photos.Select(p=>p.Url).SingleOrDefault(),
                    Age = x.GetAge()


                }).SingleOrDefaultAsync();
        }

        public  Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
           return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                        .Include(p=>p.Photos)
                        .SingleOrDefaultAsync(x=>x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                            .Include(p=>p.Photos)
                            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
             _context.Entry(user).State = EntityState.Modified;
        }
    }
}