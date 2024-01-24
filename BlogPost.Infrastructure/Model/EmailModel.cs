using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Infrastructure.Model
{
    public class EmailModel
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipient { get; set; }
        public string Passcode { get; set; }
        public static EmailModel CreateInstance(string sender, string recipient)
        {
            return new EmailModel
            {
                Sender = sender,
                Subject = "Welcome Onboard!!!!!",
                Recipient = recipient,
            };
        }
    }
}
