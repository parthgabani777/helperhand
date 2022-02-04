using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace helperland.Models.ViewModel
{
    public class loginView
    {
        [Required(ErrorMessage = "*Email is required.")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "*Enter valid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Password is required.")]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
