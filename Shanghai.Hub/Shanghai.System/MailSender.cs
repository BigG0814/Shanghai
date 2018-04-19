using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Shanghai.System
{
    public class MailSender
    {


        private MailAddress fromAddress
        {
            get
            {
                return new MailAddress("ShanghaiAppSystem@gmail.com"); //store gmail we created for sending confirmations
            }
        }
        private MailAddress toAddress { get; set; }
        private const string fromPassword = "Shanghai783!"; //gmail password for above email
        private string subject { get; set; }
        private string body { get; set; }
        private SmtpClient client { get; set; }

        public MailSender(string toaddress, string Subject, string Body)
        {
            toAddress = new MailAddress(toaddress);
            subject = Subject;
            body = Body;
            client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
        }

        public void SendMail()
        {
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })

            {
                message.IsBodyHtml = true;
                client.Send(message);
            }
        }

    }
}
