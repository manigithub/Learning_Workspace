using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadMe
{
    public struct UserDetails
    {
        public readonly String UserId;
        public readonly String Password;
        public readonly String Domain;

        public UserDetails(String userId, String password, String domain)
        {
            UserId = userId;
            Password = password;
            Domain = domain;
        }
    }
}
