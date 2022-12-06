using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TestProject.WebAPI.Data
{
    /// <summary>
    /// Account
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
