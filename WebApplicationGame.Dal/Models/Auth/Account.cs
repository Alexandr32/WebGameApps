using System;

namespace WebApplication.Model.Auth
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public Role[] Roles { get; set; }
    }

    public enum Role
    {
        User,
        Admin,
    }
}
