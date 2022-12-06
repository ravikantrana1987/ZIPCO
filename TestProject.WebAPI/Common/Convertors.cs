using System.Collections;
using System.Collections.Generic;
using TestProject.WebAPI.Data;
using TestProject.WebAPI.DTO;

namespace TestProject.WebAPI.Common
{
    public static class Convertors
    {
        public static User ToUser(this UserDto user)
        {
            return new User
            {
                UserId = user.Id,
                Email = user.Email,
                MonthlyExpenses = user.MonthlyExpenses,
                MonthlySalary = user.MonthlySalary,
                Name = user.Name
            };
        }

        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.UserId,
                Email = user.Email,
                MonthlyExpenses = user.MonthlyExpenses,
                MonthlySalary = user.MonthlySalary,
                Name = user.Name
            };
        }

        public static List<UserDto> ToUsers(this List<User> users)
        {
            if (users == null)
                return null;
            List<UserDto> userList = new List<UserDto>();
            foreach (var user in users)
            {
                userList.Add(new UserDto
                {
                    Id =user.UserId,
                    Email = user.Email,
                    MonthlyExpenses=user.MonthlyExpenses,
                    MonthlySalary=user.MonthlySalary,
                    Name = user.Name
                });
            }
            return userList;
        }
    }
}
