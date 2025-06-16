using System.ComponentModel.DataAnnotations;

namespace NetDemo.Models
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string UserName { get; set; }
    }
}
