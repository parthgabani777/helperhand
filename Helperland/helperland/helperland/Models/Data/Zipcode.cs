﻿namespace helperland.Models.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Zipcode")]
    public partial class Zipcode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ZipcodeValue { get; set; }

        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Zipcodes")]
        public virtual City City { get; set; }
    }
}
