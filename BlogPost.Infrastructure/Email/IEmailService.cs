using BlogPost.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Infrastructure.Email
{
    public interface IEmailService
    {
        bool SendEmail(EmailModel model);
        public bool SendWelcomeMessage(string recipient, string firstName);
    }
}
