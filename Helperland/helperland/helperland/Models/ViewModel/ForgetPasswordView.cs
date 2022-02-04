using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace helperland.Models.ViewModel
{
    public class ForgetPasswordView
    {
        [Required(ErrorMessage = "*Email is required.")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "*Enter valid email format")]
        public string Email { get; set; }
    }
}
