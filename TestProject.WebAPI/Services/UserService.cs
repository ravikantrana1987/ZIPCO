using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq; 
using System.Threading.Tasks;
using TestProject.WebAPI.Data; 
using TestProject.WebAPI.Interfaces;

namespace TestProject.WebAPI.Services
{
    [ExcludeFromCodeCoverage]
    public class UserService : IUserService
    {
        private readonly ZipDbContext _dbContext;
        public UserService(ZipDbContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public async Task<User> GetUserById(int id)
        {
            var user = await _dbContext.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<int> CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }
    }
}
