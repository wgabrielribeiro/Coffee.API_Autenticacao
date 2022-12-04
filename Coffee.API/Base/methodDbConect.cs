using System;
using System.Xml;

namespace Coffee.API.Base
{
    public class methodDbConect
    {
        public string fnReadXML(string NM_BANCO)
        {
            try
            {
                //Informando o caminho onde esta salvo o arquivo xml
                XmlDocument doc = new XmlDocument();
                string connectionString = "";
                string nameServer = "";
                string nameBD = "";
                string userBd = "";
                string passwordBd = "";

                string xmlBanco = @"C:\WORKSPACE\Config\Project_Gabriel.xml";
                doc.Load(xmlBanco);

                XmlNode root = doc.DocumentElement;
                XmlNodeList nodelist = root.SelectNodes(string.Format("{0}/BANCO_DADOS", NM_BANCO));

                foreach (XmlNode node in nodelist)
                {
                    nameServer = node.SelectSingleNode("NOME_SERVER").InnerText;
                    nameBD = node.SelectSingleNode("NOME_BD").InnerText;
                    userBd = node.SelectSingleNode("USUARIO").InnerText;
                    passwordBd = node.SelectSingleNode("SENHA").InnerText;
                }

                connectionString = string.Format("Data source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", nameServer, nameBD, userBd, passwordBd);

                return connectionString;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
