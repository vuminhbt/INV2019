namespace INV2019
{
    public class InOutINVInfo
    {
        public int ID { get; set; }
        public TrantportInfo TrantportInfo { get; set; }
        public string DESCRIPTION { get; set; }
        public string TIMEIN { get; set; }
        public string TIMEOUT { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

