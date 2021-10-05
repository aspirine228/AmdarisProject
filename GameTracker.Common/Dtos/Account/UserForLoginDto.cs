using System.ComponentModel.DataAnnotations;

namespace GameTracker.Common.Dtos.Account
{
    public class UserForLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
