using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTask.Models
{
    public class AccountViewModel
    {
        public class LoginViewModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }
    }
}