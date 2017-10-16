using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEE_API.Models
{
    public class AD_History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long HistoryID { get; set; }
        public int? MenuID { get; set; }
        public string MenuName { get; set; }
        public string EventType { get; set; }
        public string RecordID { get; set; }
        public string NewValues { get; set; }
        public string OriginalValues { get; set; }
        public string UserID { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
