using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationGame.Core.Model.Auth
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
