﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace helperland.Models.ViewModel
{
    public class ZipCodeView
    {
        [StringLength(20)]
        public string ZipCode { get; set; }
    }
}
