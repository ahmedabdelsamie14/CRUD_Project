using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{

			var client = new SmtpClient("smtp.gmail.com",587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("ahmed20shaheenuwk@gmail.com", "qxni jgte clxd bpck\r\n");
			client.Send("ahmed20shaheenuwk@gmail.com", email.Recipents , email.Subject, email.Body);

		}
	}
}
