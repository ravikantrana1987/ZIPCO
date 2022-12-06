using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TestProject.WebAPI.Data
{
    /// <summary>
    /// User
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public decimal MonthlySalary { get; set; }

        [Required]
        public decimal MonthlyExpenses { get; set; }
    }
}
