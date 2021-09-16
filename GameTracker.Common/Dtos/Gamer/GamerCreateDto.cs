using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace GameTracker.Common.Dtos.Gamer
{
    public class GamerCreateDto
    {
       
        [Required]
        [Range(0, 99999)]
        public int GamesPlayed { get; set; }
        [Required]
        [Range(0,10000)]
        public int Wallet { get; set; }
        
        //[Phone]
        [StringLength(128, MinimumLength = 1, ErrorMessage = "Name is too short")]
        public string PhoneNumber { get; set; }
        
        [StringLength(128, MinimumLength = 3, ErrorMessage = "Name is too short")]
        public string Name { get; set; }
       
        [EmailAddress]
        public string Email { get; set; }
    }
}
