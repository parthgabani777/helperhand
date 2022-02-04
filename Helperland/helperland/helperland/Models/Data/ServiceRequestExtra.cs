namespace helperland.Models.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ServiceRequestExtra")]
    public partial class ServiceRequestExtra
    {
        [Key]
        public int ServiceRequestExtraId { get; set; }

        public int ServiceRequestId { get; set; }

        public int ServiceExtraId { get; set; }

        [ForeignKey(nameof(ServiceRequestId))]
        [InverseProperty("ServiceRequestExtras")]
        public virtual ServiceRequest ServiceRequest { get; set; }
    }
}
