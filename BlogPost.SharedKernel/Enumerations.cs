using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.SharedKernel
{
    public class Enumerations
    {
        public enum SignUpType
        {
            SocialMediaAccount = 1,
            Email = 2,
            CreateAccount = 3
        }

        public enum ThirdPartyType
        {
            FaceBook = 1,
            Email = 2,
            Twitter = 3,
            Google = 4
        }

    }
}
