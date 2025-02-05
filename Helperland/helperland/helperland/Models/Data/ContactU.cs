﻿namespace helperland.Models.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ContactU
    {
        [Key]
        public int ContactUsId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Subject { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Message { get; set; }

        [StringLength(100)]
        public string UploadFileName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }
    }
}
