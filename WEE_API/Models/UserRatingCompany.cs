using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class UserRatingCompany
    {
        [Key]
        [Column(Order = 0)]
        public int TypeID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int QuestionID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ConpanyID { get; set; }

        [Key]
        [Column(Order = 3)]
        public string UserID { get; set; }
        public decimal? Score { get; set; }

        public bool WorkingStatus { get; set; }
        public int   WorkingType { get; set; }
        public string Salary { get; set; }
        public string YearOfEmployee { get; set; }
        public string Contract { get; set; }
    }
}