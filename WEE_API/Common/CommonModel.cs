namespace WEE_API.Common
{
    public class CommonModel
    {
        public string label { get; set; }
        public int value { get; set; }
    }

    public class MultipleCheckboxClass
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class ExternalFilterSettings
    {
        public string ScreenTitle { get; set; }
        public string FilterOn { get; set; }
        public string FilterTitleBox { get; set; }
        public string FilterTitleClass { get; set; }
    }
}