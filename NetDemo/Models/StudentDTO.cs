using NetDemo.Validations;
using System.ComponentModel.DataAnnotations;

namespace NetDemo.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Student name is required")]
        public string StudentName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [DateCheck]
        public DateTime AdmissionDate { get; set; }
    }
}
