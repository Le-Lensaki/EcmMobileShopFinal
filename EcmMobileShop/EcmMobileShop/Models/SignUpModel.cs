using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace EcmMobileShop.Models
{
    public class SignUpModel
    {
        [Key]
        public string id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "SDT")]
        public string SDT { get; set; } = string.Empty;

        [Display(Name = "Diachi")]
        public string Diachi { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Roles")]
        public string Roles { get; set; } = "KH";

        [Display(Name = "Status")]
        public bool Status { get; set; } = true;
    }

}