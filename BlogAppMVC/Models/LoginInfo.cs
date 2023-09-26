using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogAppMVC.Models
{
    public class LoginInfo
    {
        [Key]
        [Required(ErrorMessage = "Please Enter Your EmailId")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password")]
        public string Password { get; set; }
    }
}