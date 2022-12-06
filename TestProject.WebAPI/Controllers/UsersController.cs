using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Common;
using TestProject.WebAPI.DTO;
using TestProject.WebAPI.Interfaces;

namespace TestProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private int statusCode;
        private string error = string.Empty;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        } 

        #region Endpoints
        /// <summary>
        /// GET User List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<UserDto>>>> Get()
        {
            //Get User List from DB
            var users = await _userService.GetUsers();
            return StatusCode(StatusCodes.Status200OK,
                new ApiResponse<List<UserDto>>
                {
                    Data = users.ToUsers(),
                    StatusCode = StatusCodes.Status200OK,
                    Error = null
                });
        }

        /// <summary>
        /// GET User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Get(int id)
        {
            //Get User with user Id
            var user = await _userService.GetUserById(id);
            statusCode = user == null ? StatusCodes.Status204NoContent : StatusCodes.Status200OK;
            return StatusCode(statusCode,
                new ApiResponse<UserDto>
                {
                    Data = user != null ? user.ToUserDto() : new UserDto() { },
                    StatusCode = statusCode,
                    Error = error
                });
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserDto>>> Post([FromBody] UserDto user)
        {
            if (user != null && ModelState.IsValid)
            {
                //Check if user with same email exists in the DB
                var existingUser = await _userService.GetUserByEmail(user.Email);
                if (existingUser == null)
                {
                    //Create user with request body object
                    var response = await _userService.CreateUser(user.ToUser());
                    statusCode = response > 0 ? StatusCodes.Status201Created : StatusCodes.Status500InternalServerError;
                }
                else
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    error = Constants.EmailAlreadyExists;
                }
            }
            else
            {
                statusCode = StatusCodes.Status400BadRequest;
                error = Constants.InvalidUserObject;
            }

            //Construct API Response
            return StatusCode(statusCode,
                new ApiResponse<UserDto>
                {
                    Data = statusCode == StatusCodes.Status201Created ? user : null,
                    StatusCode = statusCode,
                    Error = error
                });
        }
        #endregion

    }
}
