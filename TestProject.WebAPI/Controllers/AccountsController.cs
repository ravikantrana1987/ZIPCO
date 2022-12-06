using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Common;
using TestProject.WebAPI.Data;
using TestProject.WebAPI.DTO;
using TestProject.WebAPI.Interfaces;

namespace TestProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private int statusCode;
        private string error = string.Empty;

        public AccountsController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService; 
        } 

        #region Endpoints

        /// <summary>
        /// Get Account Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Account>>>> Get()
        {
            var accountList = await _accountService.GetAccounts();
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<Account>>
            {
                Data = accountList,
                Error = string.Empty,
                StatusCode=StatusCodes.Status200OK
            });
        }

        /// <summary>
        /// Create Account
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Account>>> Post([FromBody] UserDto userRequest)
        {
            Account account = new Account();

            //check user exist in the DB
            var user = await _userService.GetUserById(userRequest.Id);
            if (user == null)
            {
                statusCode = StatusCodes.Status400BadRequest;
                error = string.Format(Constants.NoUserExist, userRequest.Id);
            }
            else
            {
                if ((user.MonthlySalary - user.MonthlyExpenses) >= Constants.BalanceAmount)
                {
                    account.UserId = userRequest.Id;
                    var response = await _accountService.CreateAccount(account);
                    statusCode = response > 0 ? StatusCodes.Status201Created : StatusCodes.Status500InternalServerError;
                }
                else
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    error = string.Format(Constants.AccountNotCreated, Constants.BalanceAmount);
                }
            }

            return StatusCode(statusCode, new ApiResponse<Account>
            {
                Data = account,
                Error = error,
                StatusCode = statusCode
            });
        }

        #endregion
    }
}
