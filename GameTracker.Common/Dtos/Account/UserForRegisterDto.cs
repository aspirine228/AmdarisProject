using System.ComponentModel.DataAnnotations;

namespace GameTracker.Common.Dtos.Account
{
    public class UserForRegisterDto
    {
        [Required]        
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
       // [Phone]
        public string PhoneNumber { get; set; }
    
    }
}
