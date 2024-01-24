using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlogPost.SharedKernel.Enumerations;

namespace BlogPost.Infrastructure.Model
{
    public class ThirdPartyRequest
    {
        public ThirdPartyType ThirdPartyType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
