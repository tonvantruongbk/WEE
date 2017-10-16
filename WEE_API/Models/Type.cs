using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class QuestionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionTypeID { get; set; }
        
        [MaxLength(500)]
        public string QuestionTypeName { get; set; }
    }
}