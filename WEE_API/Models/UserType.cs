using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class UserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTypeID { get; set; }

        [MaxLength(200)]
        public string UserTypeName { get; set; }
    }
}