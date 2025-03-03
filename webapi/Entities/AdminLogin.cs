using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
    public class AdminLogin
    {
        [Key]
        public int AdminID { get; set; }

        [Required]
        [MaxLength(50)]
        public string userID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(8)]
        public string Password { get; set; }

        
    }
}
