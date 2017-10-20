using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class CompanyZone
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name ="Công ty")]
        [Required]
        public int CompanyID { get; set; }
        [Key]
        [Column(Order = 1)]
        [Display(Name ="Lĩnh vực")]
        [Required]
        public int ZoneID { get; set; }

        [Display(Name = "Yêu cầu công việc")]
        [MaxLength(1000)]
        public string JobTypeRequire { get; set; }


        [ForeignKey("CompanyID")]
        public Company Company { get; set; }

        [ForeignKey("ZoneID")]
        public Zone Zone { get; set; }
    }


}