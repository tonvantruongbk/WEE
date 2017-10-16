using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class Zone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ZoneID { get; set; }
        [MaxLength(300)]
        public string ZoneName { get; set; }
    }


}