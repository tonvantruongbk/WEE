using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace WEE_API.Models
{
    public class AD_User
    {
        [Key]
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Password { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string PositionTitle { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public DateTime? Anniversary { get; set; }
        public string OfficePhone { get; set; }
        public string HandPhone { get; set; }
        public string HomePhone { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool? CanDelete { get; set; }

        public virtual ICollection<AD_User_Menu> AD_User_Menu { get; set; }
    }
}
