using Coffee.API.Base;
using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace Coffee.API.Model.Login
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinario, string assunto, int usuarioId, string code)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinario.Select(d => new MailboxAddress(d)));
            Assunto = assunto;

            EmailConfigReadXML _config = new EmailConfigReadXML();
            string LinkAPI = _config.fnRetornaXML("LinkAPIEmail", "Link");
            string pHyperLink = $"{LinkAPI}UsuarioId={usuarioId}&CodigoAtivacao={code}";
            string txtBody = $"<a href='{pHyperLink}'>Clique aqui para ativar seu cadastro</a>";

            Conteudo = txtBody;

            //"http://localhost:5000/api/Cadastros/Ativa?UsuarioId={usuarioId}&CodigoAtivacao={code}"
        }

    }
}
