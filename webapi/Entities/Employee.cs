using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [MaxLength(50)]
        public string JobTitle { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }

        public decimal Salary { get; set; }
        public Department? Department { get; set; }
    }
}
