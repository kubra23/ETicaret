using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ETicaret.BL.Services
{
   public static class MailService
	{
        public static bool Send(string email,string subject,string body)
        {
			try
			{
				MailMessage mail = new MailMessage();
				mail.IsBodyHtml = true;
				mail.To.Add(email);
				mail.From = new MailAddress("kubramogul06@gmail.com", "Kübra Moğul", System.Text.Encoding.UTF8);
				mail.Subject = subject;
				mail.Body = body;

				SmtpClient smtp = new SmtpClient();
				smtp.Credentials=new NetworkCredential("kubramogul06@gmail.com", "2123kubra");
				smtp.Port = 587;
				smtp.Host = "smtp.gmail.com";
				smtp.EnableSsl = true;
				smtp.Send(mail);
				return true;

			}
			catch (Exception ex)
			{

				return false;
			}
        }
    }
}
