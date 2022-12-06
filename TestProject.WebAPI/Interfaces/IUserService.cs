using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Data;

namespace TestProject.WebAPI.Interfaces
{
    public interface IUserService
    {
        //int CreateUser(User user);
        //IEnumerable<User> GetUsers();
        //User GetUserById(int id);
        //User GetUserByEmail(string email);


        Task<int> CreateUser(User user);
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
    }
}
