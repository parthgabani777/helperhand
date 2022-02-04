using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace helperland.Models.ViewModel
{
    public class ResetPasswordView
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "*Password is required.")]
        [StringLength(100)]
        public string Password { get; set; }

        [Required(ErrorMessage = " * Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "*Confirm password and password does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
