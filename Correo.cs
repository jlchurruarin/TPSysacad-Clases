using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;

namespace BibliotecaClases
{
    public static class Correo
    {
        static private string correoOrigen = "Sysacad@utn.com";

        public async static void EnviarCorreo(string destinatario, string titulo, string body)
        {
            //funcion que enviará correos.

            string patronCorreo = @"^[\w._%+-]+@[\w.-]+\.[a-zA-Z]{2,4}$";
            Regex rgCorreo = new Regex(patronCorreo);

            if (!rgCorreo.IsMatch(destinatario)) { throw new Exception("Correo electronico del destinatario no valido"); }

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("tpsysacad@gmail.com");
                mail.To.Add(destinatario);
                mail.Subject = titulo;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("tpsysacad@gmail.com", "qrcl tmum wiqh cfvd");
                    smtp.EnableSsl = true;
                    try { 
                        smtp.SendAsync(mail, null);
                    } 
                    catch (Exception ex)
                    {
                        throw new SmtpException("Error al enviar correo electronico");
                    }
                }
            }
        }

    }
}
