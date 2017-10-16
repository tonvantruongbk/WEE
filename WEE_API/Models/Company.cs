using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public byte[] Logo { get; set; }

        [Display(Name ="Lĩnh vực doanh nghiệp")]
        public int ZoneID { get; set; }
        [Display(Name ="Vị trí")]
        public int LocationID { get; set; }
        [Display(Name ="Điểm trung bình")]
        public decimal AverageScore { get; set; }
        [Display(Name ="Số người bình chọn")]
        public int   TotalUserRate { get; set; }

        [ForeignKey("ZoneID")]
        public Zone Zone { get; set; }

        [ForeignKey("LocationID")]
        public Location Location { get; set; }

    }
}