using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class UserRatingCompany
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name ="Loại câu hỏi")]
        public int QuestionTypeID { get; set; }
        [Key]
        [Column(Order = 1)]
        [Display(Name ="Câu hỏi")]
        public int QuestionID { get; set; }
        [Key]
        [Column(Order = 2)]
        [Display(Name ="Công ty")]
        public int CompanyID { get; set; }

        [Key]
        [Column(Order = 3)]
        [Display(Name ="Người bình chọn")]
        public string UserID { get; set; }
        [Display(Name ="Điểm số")]
        public decimal? Score { get; set; }

        [Display(Name ="Tình trạng công việc")]
        public bool WorkingStatus { get; set; }
        [Display(Name ="Loại hình công việc")]
        public int   WorkingType { get; set; }
        [MaxLength(300)]
        [Display(Name ="Mức lương")]
        public string Salary { get; set; }
        [MaxLength(200)]
        [Display(Name ="Thời điểm làm việc")]
        public string YearOfEmployee { get; set; }
        [MaxLength(200)]
        [Display(Name ="File hợp đồng")]
        public string Contract { get; set; }


        [ForeignKey("CompanyID")]
        public Company Company { get; set; } 
        [ForeignKey("QuestionID")]
        public Question Question { get; set; }
        [ForeignKey("QuestionTypeID")]
        public QuestionType QuestionType { get; set; }
        [ForeignKey("UserID")]
        public AD_User AD_User { get; set; }

    }
}