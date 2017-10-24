using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WEE_API.Models
{
    public class AnswerDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã chi tiết câu trả lời")]
        public int AnswerDetailID { get; set; }

        [MaxLength(500)]
        [Display(Name = "Tên chi tiết câu trả lời")]
        [Required]
        public string AnswerDetailName { get; set; }


        [Display(Name = "Thuộc câu trả lời")]
        [Required]
        public int AnswerID { get; set; }

        [ForeignKey("AnswerID")]
        public virtual Answer Answer { get; set; }
    }
}