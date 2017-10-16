using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class JobType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobTypeID { get; set; }

        [MaxLength(300)]
        public string JobTypeName { get; set; }
    }


}