using System;
using System.Xml;

namespace Coffee.API.Base
{
    public class EmailConfigReadXML
    {
        public EmailConfiguration fnRetornaEmailConfig(string EmailSettings)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string From = "";
                string SmtpServer = "";
                string Port = "";
                string Password = "";

                string xmlBanco = @"C:\WORKSPACE\Config\Project_Gabriel.xml";
                doc.Load(xmlBanco);

                XmlNode root = doc.DocumentElement;
                XmlNodeList nodelist = root.SelectNodes(string.Format("{0}", EmailSettings));

                foreach (XmlNode node in nodelist)
                {
                    From = node.SelectSingleNode("From").InnerText;
                    SmtpServer = node.SelectSingleNode("SmtpServer").InnerText;
                    Port = node.SelectSingleNode("Port").InnerText;
                    Password = node.SelectSingleNode("Password").InnerText;
                }

                EmailConfiguration objRetorno = new EmailConfiguration()
                {
                    From = From,
                    SmtpServer = SmtpServer,
                    Port = Port,
                    Password = Password
                };

                return objRetorno;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter objeto para o email configuration: " + e.Message);
            }
        }

        public string fnRetornaXML(string value, string pLink)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string Link = string.Empty;

                string xmlBanco = @"C:\WORKSPACE\Config\Project_Gabriel.xml";
                doc.Load(xmlBanco);

                XmlNode root = doc.DocumentElement;
                XmlNodeList nodelist = root.SelectNodes(string.Format("{0}", value));

                foreach (XmlNode node in nodelist)
                {
                    Link = node.SelectSingleNode(pLink).InnerText;
                }

                return Link;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter objeto para o email configuration: " + e.Message);
            }
        }
    }
}
