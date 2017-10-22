using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class JobType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Mã loại công việc")]
        public int JobTypeID { get; set; }

        [MaxLength(300)]
        [Display(Name ="Tên loại công việc")]
        public string JobTypeName { get; set; }


    }
}