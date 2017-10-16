using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WEE_API.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Mã câu hỏi")]
        public int QuestionID { get; set; }
        [MaxLength(1000)]
        [Display(Name ="Câu hỏi")]
        public string QuestionName { get; set; }

    }
}