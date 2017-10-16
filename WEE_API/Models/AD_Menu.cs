using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

namespace WEE_API.Models
{
    public   class AD_Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuID { get; set; }
        public int? MenuParentID { get; set; }
        public string MenuText { get; set; }
        public string URLAction { get; set; }
        public string MenuIcon { get; set; }
        public int? MenuSort { get; set; }
        public bool? MenuSeparator { get; set; }
        public bool? CanDelete { get; set; }
    
        public virtual ICollection<AD_User_Menu> AD_User_Menu { get; set; }
    }
}
