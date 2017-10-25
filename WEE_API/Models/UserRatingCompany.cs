using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WEE_API.RBAC;

namespace WEE_API.Models
{
    public class UserRatingCompany
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Câu hỏi")]
        public int QuestionID { get; set; }
        [Key]
        [Column(Order = 1)]
        [Display(Name ="Câu trả lời")]
        public int AnswerID { get; set; }
      
        [Key]
        [Column(Order = 2)]
        [Display(Name ="Công ty")]
        public int CompanyID { get; set; }

        [Key]
        [Column(Order = 3)]
        [Display(Name ="Người bình chọn")]
        public int UserID { get; set; }

       
        [Display(Name = "chi tiết trả lời")]
        public int AnswerDetailID { get; set; }

        [Display(Name ="Điểm bình chọn")]
        public decimal? Score { get; set; }

        [Display(Name ="Mô tả vấn đề")]
        public string ProblemDescription { get; set; }
        [Display(Name ="Nguyên nhân")]
        public string ProblemCauseBy { get; set; }
        [Display(Name ="Đề xuất")]
        public string Suggetion { get; set; }

        [Display(Name ="Nguyện vọng")]
        public int Aspiration { get; set; }

        [Display(Name ="Đính kèm")]
        public string Attachments { get; set; }


        [ForeignKey("CompanyID")]
        [JsonIgnore]
        public Company Company { get; set; } 
        [ForeignKey("QuestionID")]
        [JsonIgnore]
        public Question Question { get; set; }
        [ForeignKey("AnswerID")]
        [JsonIgnore]
        public Answer Answer { get; set; }
        [ForeignKey("UserID")]
        [JsonIgnore]
        public ApplicationUser Users { get; set; }

    }
}