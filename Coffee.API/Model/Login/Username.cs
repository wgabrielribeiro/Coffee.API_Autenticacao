namespace Coffee.API.Model.Login
{
    public class Username
    {
        public string Usuario { get; set; }
        public string Token { get; set; }

    }

    public class RetornoToken
    {
        public string Usuario { get; set; }
        public string Email { get; set; }
    }

}
