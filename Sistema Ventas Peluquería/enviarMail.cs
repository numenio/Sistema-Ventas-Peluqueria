using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace ProcesosNegocio
{
    public class enviarMail
    {
        
        public static void enviar(string to, string from, string asunto, string cuerpo, string servidor, int puerto, string usuario, string clave)
        {

            MailMessage msg = new MailMessage();

            msg.To.Add(new MailAddress(to));
            msg.From = new MailAddress(from);
            msg.Subject = asunto;
            msg.Body = cuerpo;

            SmtpClient clienteSmtp = new SmtpClient(servidor, puerto);

            clienteSmtp.Credentials = new NetworkCredential(usuario, clave);
            clienteSmtp.EnableSsl = true;

            try
            {
                clienteSmtp.Send(msg);
            }

            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                msg.Dispose();
            }

        }
    }
}
