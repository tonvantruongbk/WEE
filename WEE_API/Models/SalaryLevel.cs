using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class SalaryLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã mức lương")]
        public int SalaryLevelID { get; set; }

        [MaxLength(200)]
        [Display(Name = "Tên mức lương")]
        [Required]
        public string SalaryLevelName { get; set; }
    }
}