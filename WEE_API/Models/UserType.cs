using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class UserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Mã nhóm người dùng")]
        public int UserTypeID { get; set; }

        [MaxLength(200)]
        [Display(Name ="Tên nhóm người dùng")]
        public string UserTypeName { get; set; }
    }
}