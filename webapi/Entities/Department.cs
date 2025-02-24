using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        [Required]
        [MaxLength(50)]
        public string DepartmentName { get; set; }
    }
}
