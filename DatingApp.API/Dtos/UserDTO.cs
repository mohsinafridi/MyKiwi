using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserDTO
    { 
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="Your password should b/w 4-8 characters.")]
        public string Password { get; set; }
    
    }
}