using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Playground.WrapperPattern
{
    public class EmailCreator : IEmailFluentInterface
    {
        private MailMessage _mailMessage = new MailMessage();

        private EmailCreator(string fromAddress)
        {
            _mailMessage.Sender = new MailAddress(fromAddress);
        }

        public static IEmailFluentInterface CreateEmailFrom(string fromAddress)
        {
            return new EmailCreator(fromAddress);
        }

        public IEmailFluentInterface To(params string[] toAddresses)
        {
            foreach (string toAddress in toAddresses)
            {
                _mailMessage.To.Add(new MailAddress(toAddress));
            }
            return this;
        }

        public IEmailFluentInterface CC(params string[] ccAddresses)
        {
            foreach (string ccAddress in ccAddresses)
            {
                _mailMessage.CC.Add(new MailAddress(ccAddress));
            }
            return this;
        }

        public IEmailFluentInterface BCC(params string[] bccAddresses)
        {
            foreach (string bccAddress in bccAddresses)
            {
                _mailMessage.Bcc.Add(new MailAddress(bccAddress));
            }
            return this;
        }

        public IEmailFluentInterface WithSubject(string subject)
        {
            _mailMessage.Subject = subject;
            return this;
        }

        public IEmailFluentInterface WithBody(string body)
        {
            _mailMessage.Body = body;
            return this;
        }

        public void Send()
        {
            Console.WriteLine("FROM : ");
            Console.WriteLine(String.Format("\t{0}", _mailMessage.Sender.ToString()));
            Console.WriteLine();
            Console.WriteLine("TO : ");
            foreach (MailAddress addr in _mailMessage.To)
            {
                Console.WriteLine(String.Format("\t{0}", addr.ToString()));
            }
            Console.WriteLine();
            Console.WriteLine("CC : ");
            foreach (MailAddress addr in _mailMessage.CC)
            {
                Console.WriteLine(String.Format("\t{0}", addr.ToString()));
            }
            Console.WriteLine();
            Console.WriteLine("BCC : ");
            foreach (MailAddress addr in _mailMessage.Bcc)
            {
                Console.WriteLine("\t{0}", addr.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("SUBJECT : ");
            Console.WriteLine(String.Format("\t{0}", _mailMessage.Subject.ToString()));
            Console.WriteLine();
            Console.WriteLine("BODY : ");
            Console.WriteLine(String.Format("\t{0}", _mailMessage.Body.ToString()));
            Console.ReadLine();
        }
    }
}