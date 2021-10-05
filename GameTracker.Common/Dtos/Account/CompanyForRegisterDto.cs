using System.ComponentModel.DataAnnotations;


namespace GameTracker.Common.Dtos.Account
{
    public class CompanyForRegisterDto
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string CompanyEmail { get; set; }

    }
}
