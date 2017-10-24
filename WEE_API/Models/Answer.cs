using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WEE_API.Models
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã câu trả lời")]
        public int AnswerID { get; set; }

        [MaxLength(500)]
        [Display(Name = "Tên câu trả lời")]
        [Required]
        public string AnswerName { get; set; }


        public  virtual  ICollection<AnswerDetail> ListAnswerDetail { get; set; }
    }
}