namespace INV2019
{
    public class TrantportInfo
    {
        public int ID { get; set; }
        public string BARCODE { get; set; }
        public string CARNUMBER { get; set; }
        public UserInfo CREATEBY { get; set; }
        public string COMPANY { get; set; }
        
        public override string ToString()
        {
            return string.Format("{0} - {1}", this.BARCODE, this.CARNUMBER);
        }
    }
}

