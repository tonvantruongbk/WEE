using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class UserRatingCompany
    {
        [Key]
        [Column(Order = 0)]
        public int QuestionTypeID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int QuestionID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int CompanyID { get; set; }

        [Key]
        [Column(Order = 3)]
        public string UserID { get; set; }
        public decimal? Score { get; set; }

        public bool WorkingStatus { get; set; }
        public int   WorkingType { get; set; }
        [MaxLength(300)]
        public string Salary { get; set; }
        [MaxLength(200)]
        public string YearOfEmployee { get; set; }
        [MaxLength(200)]
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