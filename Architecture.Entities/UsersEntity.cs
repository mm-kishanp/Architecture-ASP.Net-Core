using System;
using System.ComponentModel.DataAnnotations;

namespace Architecture.Entities
{
    /// <summary>
    /// This will be have the View model details
    /// </summary>
    public class UsersEntity
    {
        public long Id { get; set; }
        [Display(Name ="Active")]
        public bool IsActive { get; set; }
        [Display(Name ="Approved")]
        public bool IsApproved { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? UpdatedUtcdate { get; set; }
        public DateTime? CreatedUtcdate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Required]
        [Display(Name ="First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name ="Last name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name ="Email address")]
        public string EmailId { get; set; }
        [Required]
        [Phone]
        [Display(Name ="Contact Number")]
        public string ContactNo { get; set; }
        [Required]
        [Display(Name ="Password")]
        public string Password { get; set; }
        public string ProfilePic { get; set; }
        public string LoginAttempt { get; set; }
    }
}
