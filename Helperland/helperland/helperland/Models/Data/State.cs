﻿namespace helperland.Models.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("State")]
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string StateName { get; set; }

        [InverseProperty(nameof(City.State))]
        public virtual ICollection<City> Cities { get; set; }
    }
}
