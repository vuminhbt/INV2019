namespace INV2019
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string USERNAME { get; set; }
        public string PASS { get; set; }
        public string ACTIVE { get; set; }
       

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

