using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.WrapperPattern
{
    public interface IEmailFluentInterface
    {
        IEmailFluentInterface To(params string[] toAddresses);
        IEmailFluentInterface CC(params string[] ccAddresses);
        IEmailFluentInterface BCC(params string[] bccAddresses);
        IEmailFluentInterface WithSubject(string subject);
        IEmailFluentInterface WithBody(string body);
        void Send();
    }
}