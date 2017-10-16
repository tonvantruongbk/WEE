using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class AD_AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AuditLogID { get; set; }
        [ForeignKey("AD_History")]
        public long HistoryID { get; set; }
        public string UserID { get; set; }
        public string MenuName { get; set; }
        public string EventType { get; set; }
        public string Descriptions { get; set; }
        public DateTime? AccessedDateTime { get; set; }

        public virtual AD_History AD_History { get; set; }
    }
}
