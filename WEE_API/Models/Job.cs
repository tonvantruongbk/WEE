using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã công việc")]
        public int JobID { get; set; }

        [MaxLength(500)]
        [Display(Name = "Tên công việc")]
        public string JobName { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? DateCreate { get; set; }


        [Display(Name = "Ngày kết thúc")]
        public DateTime? DateEnd { get; set; }

        public virtual List<CompanyJob> ListCompanyJob { get; set; }


    }
}