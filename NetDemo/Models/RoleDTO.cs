using System.ComponentModel.DataAnnotations;

namespace NetDemo.Models
{
    public class RoleDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
