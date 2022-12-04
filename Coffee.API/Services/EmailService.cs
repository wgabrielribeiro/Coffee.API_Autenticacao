using Coffee.API.Base;
using Coffee.API.Model.Login;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;

namespace Coffee.API.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;
        private EmailConfigReadXML _configEmail;

        public EmailService(IConfiguration configuration, EmailConfigReadXML configEmail)
        {
            _configuration = configuration;
            _configEmail = configEmail;
        }

        public void EnviarEmail(string[] destinatario, string assunto, int Usuarioid, string code)
        {
            try
            {
                Mensagem mensagem = new Mensagem(destinatario, assunto,Usuarioid,code);

                var mensagemEmail = CriaCorpoEmail(mensagem);

                EnviarEmail(mensagemEmail);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EnviarEmail(MimeMessage mensagemEmail)
        {
            try
            {
                using(var cliente = new SmtpClient())
                {
                    try
                    {
                        var retorno = _configEmail.fnRetornaEmailConfig("EmailSettings");

                        if(retorno != null)
                        {
                            cliente.Connect(retorno.SmtpServer, Convert.ToInt32(retorno.Port),
                            true);

                            cliente.AuthenticationMechanisms.Remove("XOUATH2");
                            cliente.Authenticate(retorno.From, retorno.Password);

                            cliente.Send(mensagemEmail);
                        }
                        
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        cliente.Disconnect(true);
                        cliente.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private MimeMessage CriaCorpoEmail(Mensagem mensagem)
        {
            try
            {                
                var mensagemDeEmail = new MimeMessage();

                var From = _configEmail.fnRetornaEmailConfig("EmailSettings");

                if(From != null)
                {
                    mensagemDeEmail.From.Add(new MailboxAddress(From.From));
                    mensagemDeEmail.To.AddRange(mensagem.Destinatario);
                    mensagemDeEmail.Subject = mensagem.Assunto;
                    mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    {
                        Text = mensagem.Conteudo
                    };                                        
                }
                return mensagemDeEmail;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar corpo de email: " + ex.StackTrace);
            }
        }
    }
}
