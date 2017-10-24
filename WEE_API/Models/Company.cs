using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Newtonsoft.Json;
using WEE_API.Common;

namespace WEE_API.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Mã công ty")]
        public int CompanyID { get; set; }
        [Display(Name ="Tên công ty")]
        public string CompanyName { get; set; }
        [Display(Name ="Địa chỉ")]
        public string Address { get; set; }
        [Display(Name ="Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name ="Mô tả về công ty")]
        public string Description { get; set; }
        [Display(Name ="Trang web")]
        public string Website { get; set; }
        [Display(Name ="Logo")]
        public string Logo { get; set; }

        [Display(Name ="Vị trí")]
        public int? LocationID { get; set; }
       
        [Display(Name ="Điểm trung bình")]
        public decimal? AverageScore { get; set; } 
        [Display(Name ="Số người bình chọn")]
        public int? TotalUserRate { get; set; } 
        [Display(Name ="Tổng số việc làm")]
        public int? TotalJobActive { get; set; }
        

        [ForeignKey("LocationID")]
        [JsonIgnore]
        public virtual Location Location { get; set; }


        [JsonIgnore]
        public virtual ICollection<MultipleCheckboxClass> CompanyZone { get; set; }
        [JsonIgnore]
        public virtual ICollection<CompanyZone> ListCompanyZone { get; set; }
        [JsonIgnore]
        public virtual ICollection<Job> ListJob { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRatingCompany> ListUserRatingCompany { get; set; }



    }
}