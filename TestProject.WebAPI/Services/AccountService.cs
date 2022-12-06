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
    public class AccountService : IAccountService
    {
        private readonly ZipDbContext _dbContext;
        public AccountService(ZipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAccount(Account account)
        {
            _dbContext.Accounts.Add(account);
            return  await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Account>> GetAccounts()
        {
            return await _dbContext.Accounts.ToListAsync();
        } 
    }
}
