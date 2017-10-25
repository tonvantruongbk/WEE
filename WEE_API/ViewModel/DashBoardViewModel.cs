using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEE_API.ViewModel
{
    public class DashBoardViewModel
    {
        public TotalNumberInfo TotalNumberInfo { get; set; }
    }


    public class TotalNumberInfo
    {
        public int CompanyNum { get; set; }
        public decimal CompanyPer { get; set; }

        public int UserNum { get; set; }
        public int UserPer { get; set; }

        public int RateNum { get; set; }
        public int RatePer { get; set; }

        public int JobNum { get; set; }
        public int JobPer { get; set; }
    }
}