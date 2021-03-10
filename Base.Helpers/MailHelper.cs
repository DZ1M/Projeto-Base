using Base.Helpers.DTO;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;

namespace Base.Helpers
{
    public class MailHelper
    {
        private static void Send(List<DestinataryMail> destinatarios, string assunto, string texto, dynamic emailControle)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailControle.Email, emailControle.Email));
            foreach (DestinataryMail destinatario in destinatarios)
            {
                if (EmailValid(destinatario.Email))
                {
                    message.To.Add(new MailboxAddress(destinatario.Name, destinatario.Email));
                }
            }

            if (message.To.Count > 0)
            {
                message.Subject = assunto;

                message.Body = new TextPart("html")
                {
                    Text = texto
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(emailControle.Smtp, emailControle.Porta, emailControle.Ssl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailControle.Usuario, emailControle.Senha);
#if !DEBUG
                    client.Send(message);
#endif
                    client.Disconnect(true);
                }
            }
        }

        public static void SendEmail(List<DestinataryMail> destinatarios, string assunto, string texto, dynamic emailControle)
        {
            Send(destinatarios, assunto, texto, emailControle);
        }

        public static void SendEmail(string nomeDestinatario, string emailDestinatario, string assunto, string texto, dynamic emailControle)
        {
            Send(new List<DestinataryMail>() { new DestinataryMail() { Name = nomeDestinatario, Email = emailDestinatario } }, assunto, texto, emailControle);
        }
        public static bool EmailValid(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
