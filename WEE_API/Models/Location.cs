using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Mã vị trí")]
        public int LocationID { get; set; }

        [MaxLength(100)]
        
        [Display(Name ="Tên vị trí")]
        public string LocationName { get; set; }
    }
}