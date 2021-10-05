using System;
using System.ComponentModel.DataAnnotations;


namespace GameTracker.Common.Dtos.FeedBack
{
    public class CreateFeedBackDto
    {
        [Required]
        public string Text { get; set; }
        [Required]
        [Range(0, 10)]
        public float Grade { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
