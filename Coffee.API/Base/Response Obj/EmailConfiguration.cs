﻿namespace Coffee.API.Base
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
    }
}
