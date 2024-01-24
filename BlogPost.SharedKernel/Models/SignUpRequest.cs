using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlogPost.SharedKernel.Enumerations;

namespace BlogPost.SharedKernel.Models
{
    public class SignUpRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get;  set; }
        public SignUpType SignUpType { get; set; }
    }
}
