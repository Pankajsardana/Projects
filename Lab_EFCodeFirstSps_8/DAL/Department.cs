using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Department
    {
        [Key] 
        public int DepartmentId { get; set; }
        [Required] 
        public string Name { get; set; }
    }
}