using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class WorkingTime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã thời gian làm việc")]
        public int WorkingTimeID { get; set; }

        [MaxLength(200)]
        [Display(Name = "Tên thời gian làm việc")]
        [Required]
        public string WorkingTimeName { get; set; }
    }
}