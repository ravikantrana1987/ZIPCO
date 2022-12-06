using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Controllers;
using TestProject.WebAPI.Data;
using TestProject.WebAPI.DTO;
using TestProject.WebAPI.Interfaces;

namespace TestProject.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private UsersController _userController;

        [SetUp]
        public void SetUp()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UsersController(_mockUserService.Object);
        }

        #region GET
        [Test]
        public void GetUser_NullResponseTest()
        {
            //Arrange
            _mockUserService.Setup(x => x.GetUsers()).ReturnsAsync(It.IsAny<List<User>>());
            //Act
            var response = _userController.Get().Result.Result as ObjectResult;
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, ((ApiResponse<List<UserDto>>)response.Value).StatusCode);
            Assert.IsNull(((ApiResponse<List<UserDto>>)response.Value).Data);
        }

        [Test]
        public void GetUser_SuccessTest()
        {
            //Arrange
            _mockUserService.Setup(x => x.GetUsers()).ReturnsAsync(GetUserList());
            //Act
            var response = _userController.Get().Result.Result as ObjectResult;
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, ((ApiResponse<List<UserDto>>)response.Value).StatusCode);
            Assert.AreEqual(2, ((ApiResponse<List<UserDto>>)response.Value).Data.Count);
        }
        #endregion

        #region GET/{id}
        [Test]
        public void GetUser_NoUserTest()
        {
            //Arrange
            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(It.IsAny<User>());
            //Act
            var response = _userController.Get(It.IsAny<int>()).Result.Result as ObjectResult;
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(204, ((ApiResponse<UserDto>)response.Value).StatusCode);
            Assert.IsNull(((ApiResponse<UserDto>)response.Value).Data.Email);
        }

        [Test]
        public void GetUser_UserValueTest()
        {
            //Arrange
            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(GetUser());
            //Act
            var response = _userController.Get(It.IsAny<int>()).Result.Result as ObjectResult;
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, ((ApiResponse<UserDto>)response.Value).StatusCode);
            Assert.IsNotNull(((ApiResponse<UserDto>)response.Value).Data);
        }
        #endregion

        #region POST
        [Test]
        public void Post_NullRequestTest()
        {
            //Act
            var response = _userController.Post(It.IsAny<UserDto>()).Result.Result as ObjectResult;
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(400, ((ApiResponse<UserDto>)response.Value).StatusCode);
            Assert.IsNotNull(((ApiResponse<UserDto>)response.Value).Error);
        }

        [Test]
        public void Post_EmailExistsTest()
        {
            //Arrange
            _mockUserService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(GetExistingEmailUser());
            //Act
            var response = _userController.Post(GetRequestUser()).Result.Result as ObjectResult;
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(400, ((ApiResponse<UserDto>)response.Value).StatusCode);
            Assert.IsNotNull(((ApiResponse<UserDto>)response.Value).Error);
        }


        [Test]
        public void Post_CreateUserTest()
        {
            //Arrange
            _mockUserService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(It.IsAny<User>());
            _mockUserService.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync(1);
            //Act
            var response = _userController.Post(GetRequestUser()).Result.Result as ObjectResult;
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(201, ((ApiResponse<UserDto>)response.Value).StatusCode);
            Assert.IsEmpty(((ApiResponse<UserDto>)response.Value).Error);
            Assert.IsNotNull(((ApiResponse<UserDto>)response.Value).Data);
        }

        #endregion
        private static List<User> GetUserList()
        {
            return new List<User>()
           {
                new User
                {
                    UserId= 1,
                    Email="test@test.com",
                    MonthlyExpenses=1000,
                    MonthlySalary=3000,
                    Name="Test1"
                },
                new User
                {
                    UserId= 2,
                    Email="test2@test.com",
                    MonthlyExpenses=2000,
                    MonthlySalary=3000,
                    Name="Test2"
                }
           };
        }
        private static User GetUser()
        {
            return new User
            {
                UserId = 1,
                Email = "test@email.com",
                MonthlyExpenses = 1000,
                MonthlySalary = 2000,
                Name = "Test"
            };
        }

        private static UserDto GetRequestUser()
        {
            return new UserDto
            {
                Email = "test@email.com",
                Name = "Test",
                MonthlySalary = 2000,
                MonthlyExpenses = 100
            };
        }

        private static User GetExistingEmailUser()
        {
            return new User
            {
                UserId= 1,
                Email = "test@email.com",
                Name = "Test",
                MonthlySalary = 2000,
                MonthlyExpenses = 100
            };
        }
    }
}
