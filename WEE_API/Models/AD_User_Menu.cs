using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_WEB_API.Models
{
    public class AD_User_Menu
    {
        [Key, Column(Order = 0)]
        [ForeignKey("AD_User")]
        public string UserID { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("AD_Menu")]
        public int MenuID { get; set; }
        public bool? Read { get; set; }
        public bool? Add { get; set; }
        public bool? Edit { get; set; }
        public bool? Delete { get; set; }
        public bool? Excel_CSV { get; set; }
        public bool? PDF { get; set; }
        public bool? Print { get; set; }

        public virtual AD_Menu AD_Menu { get; set; }
        public virtual AD_User AD_User { get; set; }
    }
}
