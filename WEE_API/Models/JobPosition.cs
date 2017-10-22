using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class JobPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã vị trí công việc")]
        public int JobPositionID { get; set; }

        [MaxLength(200)]
        [Display(Name = "Tên vị trí")]
        [Required]
        public string JobPositionName { get; set; }
    }
}