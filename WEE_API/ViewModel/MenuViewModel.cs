using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEE_API.ViewModel
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public int? _parentId { get; set; }
        public string name { get; set; }
        public string action { get; set; }
        public string iconCls { get; set; }
        public int? order { get; set; }
    }

    public class AccessMatrixViewModel
    {
        public int MenuID { get; set; }
        public string UserID { get; set; }
        public string MenuText { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool CSV { get; set; }
        public bool Excel { get; set; }
        public bool PDF { get; set; }
        public bool Print { get; set; }
    }
}