using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Data;

namespace TestProject.WebAPI.Interfaces
{
    public interface IAccountService
    {
        //IEnumerable<Account> GetAccounts();
        //int CreateAccount(Account account);

        Task<List<Account>> GetAccounts();
        Task<int> CreateAccount(Account account);
    }
}
