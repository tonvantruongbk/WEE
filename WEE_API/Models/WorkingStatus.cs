using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class WorkingStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã tình trạng làm việc")]
        public int WorkingStatusID { get; set; }

        [MaxLength(200)]
        [Display(Name = "Tên tình trạng làm việc")]
        [Required]
        public string WorkingStatusName { get; set; }
    }
}