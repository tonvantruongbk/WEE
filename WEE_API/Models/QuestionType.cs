using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class QuestionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Mã loại câu hỏi")]
        public int QuestionTypeID { get; set; }
        
        [MaxLength(500)]
        [Display(Name ="Loại câu hỏi")]
        [Required]
        public string QuestionTypeName { get; set; }
    }
}