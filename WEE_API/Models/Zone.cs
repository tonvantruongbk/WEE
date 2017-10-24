using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WEE_API.Models
{
    public class Zone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã lĩnh vực doanh nghiệp")]
        public int ZoneID { get; set; }
        [MaxLength(300)]
        [Display(Name = "Tên lĩnh vực doanh nghiệp")]
        public string ZoneName { get; set; }


        [JsonIgnore]
        public virtual ICollection<CompanyZone> ListZoneCompany { get; set; }
    }


}