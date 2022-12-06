using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestProject.WebAPI.Controllers;
using TestProject.WebAPI.Data;
using TestProject.WebAPI.DTO;
using TestProject.WebAPI.Interfaces;

namespace TestProject.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTest
    {
        private Mock<IUserService> _mockUserService;
        private Mock<IAccountService> _mockAccountService;
        private AccountsController _accountController;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _mockAccountService = new Mock<IAccountService>();
            _accountController = new AccountsController(_mockAccountService.Object, _mockUserService.Object);

        }

        #region GET
        [Test]
        public void GetAccount_NoResponse()
        {
            _mockAccountService.Setup(x => x.GetAccounts()).ReturnsAsync(It.IsAny<List<Account>>());
            var response = _accountController.Get().Result.Result as ObjectResult;
            Assert.IsNotNull(response);
            Assert.IsNull(((ApiResponse<List<Account>>)response.Value).Data);
            Assert.AreEqual(200, ((ApiResponse<List<Account>>)response.Value).StatusCode);
        }

        [Test]
        public void GetAccount_AccountListResponse()
        {
            _mockAccountService.Setup(x => x.GetAccounts()).ReturnsAsync(GetAccountList());
            var response = _accountController.Get().Result.Result as ObjectResult;
            Assert.IsNotNull(response);
            Assert.IsNotNull(((ApiResponse<List<Account>>)response.Value).Data);
            Assert.AreEqual(2, ((ApiResponse<List<Account>>)response.Value).Data.Count);
            Assert.AreEqual(200, ((ApiResponse<List<Account>>)response.Value).StatusCode);
        }
        #endregion

        #region POST
        [Test]
        public void CreateAccount_UserNotValid()
        {
            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(It.IsAny<User>());
            var response = _accountController.Post(GetUserDtoRequest()).Result.Result as ObjectResult;
            Assert.IsNotNull(response);
            Assert.IsNotNull(((ApiResponse<Account>)response.Value).Data);
            Assert.IsNotEmpty(((ApiResponse<Account>)response.Value).Error);
            Assert.AreEqual(400, ((ApiResponse<Account>)response.Value).StatusCode);
        }

        [Test]
        public void CreateAccount_UserWithMoreExpenses()
        {
            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(GetExistingUserMoreExpenses());
            var response = _accountController.Post(GetUserDtoRequest()).Result.Result as ObjectResult;
            Assert.IsNotNull(response);
            Assert.IsNotNull(((ApiResponse<Account>)response.Value).Data);
            Assert.IsNotEmpty(((ApiResponse<Account>)response.Value).Error);
            Assert.AreEqual(400, ((ApiResponse<Account>)response.Value).StatusCode);
        }

        [Test]
        public void CreateAccount_UserWithValidExpenses()
        {
            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(GetExistingUserWithValidExpenses());
            _mockAccountService.Setup(x => x.CreateAccount(It.IsAny<Account>())).ReturnsAsync(1);
            var response = _accountController.Post(GetUserDtoRequest()).Result.Result as ObjectResult;
            Assert.IsNotNull(response);
            Assert.IsNotNull(((ApiResponse<Account>)response.Value).Data);
            Assert.IsEmpty(((ApiResponse<Account>)response.Value).Error);
            Assert.AreEqual(201, ((ApiResponse<Account>)response.Value).StatusCode);
        } 
         

        #endregion


        private UserDto GetUserDtoRequest()
        {
            return new UserDto
            {
                Id = 1,
                Email = "test@test.com",
                MonthlyExpenses = 100,
                MonthlySalary = 10000,
                Name = "Test"
            };
        }

        private User GetExistingUserMoreExpenses()
        {
            return new User
            {
                UserId = 1,
                Email = "test@test.com",
                MonthlyExpenses = 1500,
                MonthlySalary = 2000,
                Name = "Test"
            };
        }

        private User GetExistingUserWithValidExpenses()
        {
            return new User
            {
                UserId = 1,
                Email = "test@test.com",
                MonthlyExpenses = 1000,
                MonthlySalary = 2000,
                Name = "Test"
            };
        }

        private List<Account> GetAccountList()
        {
            return new List<Account>
            {
                new Account
                {
                    AccountId= 1,
                    UserId=1
                },
                new Account
                {
                    AccountId= 2,
                    UserId=2
                }
            };
        }


    }
}
