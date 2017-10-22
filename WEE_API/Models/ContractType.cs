using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class ContractType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã loại hợp đồng")]
        public int ContractTypeID { get; set; }

        [MaxLength(200)]
        [Display(Name = "Tên loại hợp đồng")]
        [Required]
        public string ContractTypeName { get; set; }
    }
}