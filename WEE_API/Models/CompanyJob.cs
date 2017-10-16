using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class CompanyJob
    {
        [Key]
        [Column(Order = 0)]
        public int CompanyID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int JobID { get; set; }


        public string JobTypeRequire { get; set; }


        [ForeignKey("CompanyID")]
        public Company Company { get; set; }

        [ForeignKey("JobID")]
        public Job Job { get; set; }
    }


}