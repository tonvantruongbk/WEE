using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public byte[] Logo { get; set; }

        public int ZoneID { get; set; }
        public int LocationID { get; set; }
        public decimal AverageScore { get; set; }
        public int   TotalUserRate { get; set; }

        [ForeignKey("ZoneID")]
        public Zone Zone { get; set; }

        [ForeignKey("LocationID")]
        public Location Location { get; set; }

    }
}