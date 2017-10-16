using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class CompanyJob
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name ="Công ty")]
        public int CompanyID { get; set; }
        [Key]
        [Column(Order = 1)]
        [Display(Name ="Công việc")]
        public int JobID { get; set; }

        [Display(Name = "Yêu cầu công việc")]
        [MaxLength(1000)]
        public string JobTypeRequire { get; set; }


        [ForeignKey("CompanyID")]
        public Company Company { get; set; }

        [ForeignKey("JobID")]
        public Job Job { get; set; }
    }


}