using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        public virtual List<CompanyZone> ListZoneCompany { get; set; }
    }


}