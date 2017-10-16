using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEE_API.ViewModel
{
    public class DatatablesEditorViewModel
    {
        public bool buttonExternal { get; set; }
        public string Controller { get; set; }
        public string FieldID { get; set; }
        public string TableID{ get; set; }
        public string ButtonPlaceID { get; set; }
        public string Ydacf { get; set; }
    }
}