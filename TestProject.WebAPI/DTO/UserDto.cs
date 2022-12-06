using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TestProject.WebAPI.DTO
{
    /// <summary>
    /// User DTO
    /// </summary>
    [ExcludeFromCodeCoverage] 
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public decimal MonthlySalary { get; set; }

        [Required]
        public decimal MonthlyExpenses { get; set; }
    }
}
