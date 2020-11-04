using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationGame.Core.Model.Auth
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public Role[] Roles { get; set; }
    }
}
