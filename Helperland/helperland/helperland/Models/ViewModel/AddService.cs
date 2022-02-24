using helperland.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace helperland.Models.ViewModel
{
    public class AddService
    {
        public AddService()
        {
            Comments = null;
            HasPets = false;
            ExtraServices = new List<int>();
        }

        [StringLength(20)]
        public string PostalCode { get; set; }

        public DateTime ServiceStartDate { get; set; }

        public double ServiceHours { get; set; }

        [StringLength(500)]
        public string? Comments { get; set; }

        public bool? HasPets { get; set; }

        public List<int>? ExtraServices { get; set; }

        public UserAddress userAddress { get; set; }
    }
}
