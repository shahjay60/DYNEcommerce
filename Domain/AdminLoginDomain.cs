namespace Domain
{
    public class AdminLoginDomain
    {
        public int AdminId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string EmailId { get; set; }
        public string phoneNo { get; set; }
        public bool RememberMe { get; set; }

    }
}
