using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Practise.Models
{
    public class Employees
    {
        [Key]
        public Guid EmpID { get; set; }
        [Required]
        [DisplayName("Employee Name:")]
        public string EmpName { get; set; }
        [Required]
        [DisplayName("Employee Address:")]
        public string EmpAddress { get; set; }
        [Phone]
        [DisplayName("Employee Phone Number:")]
        public string EmpPhone { get; set; }
        [Required]
        [DisplayName("Employee Salary:")]
        public int EmpSalary { get; set; }
        [Required]
        [DisplayName("Employee Post:")]
        public string EmpDesignation { get; set; }
        
        public string Image { get; set; }
    }
}
