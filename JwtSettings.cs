namespace NeComPlus
{
    public class JwtSettings
    {
        public string Key { get; set; }

        public string Issuer { get; set; }

        public int ExpireDays { get; set; }
    }
}