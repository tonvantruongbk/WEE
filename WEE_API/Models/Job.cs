using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobID { get; set; }
        public string JobName { get; set; }

        public DateTime? DateCreate { get; set; }
        public DateTime? DateEnd { get; set; }


    }


}