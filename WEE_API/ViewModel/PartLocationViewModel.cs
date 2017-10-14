namespace WEE_WEB_API.ViewModel
{
    public class PartLocationViewModel
    {
        public string LocationID { get; set; }
        public string Batch { get; set; }
        public string BatchDateStamp { get; set; }
        public long OnHand { get; set; }
        public long PickAllocated { get; set; }
        public long Available { get; set; }
    }
}