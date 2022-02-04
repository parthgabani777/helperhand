namespace helperland.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            FavoriteAndBlockedTargetUsers = new HashSet<FavoriteAndBlocked>();
            FavoriteAndBlockedUsers = new HashSet<FavoriteAndBlocked>();
            RatingRatingFromNavigations = new HashSet<Rating>();
            RatingRatingToNavigations = new HashSet<Rating>();
            ServiceRequestServiceProviders = new HashSet<ServiceRequest>();
            ServiceRequestUsers = new HashSet<ServiceRequest>();
            UserAddresses = new HashSet<UserAddress>();
        }

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "*Firstname is required.")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Lastname is required.")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*Email is required.")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$",ErrorMessage ="*Enter valid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Password is required.")]
        [StringLength(100)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,14}$",ErrorMessage ="*Enter valid password format")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = " * Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "*Confirm password and password does not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "*Mobile is required.")]
        [StringLength(20)]
        public string Mobile { get; set; }

        public int UserTypeId { get; set; }

        public int? Gender { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        public string UserProfilePicture { get; set; }

        public bool IsRegisteredUser { get; set; }

        [StringLength(200)]
        public string PaymentGatewayUserRef { get; set; }

        [StringLength(20)]
        public string ZipCode { get; set; }

        public bool WorksWithPets { get; set; }

        public int? LanguageId { get; set; }

        public int? NationalityId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }

        public bool IsApproved { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int? Status { get; set; }

        [StringLength(100)]
        public string BankTokenId { get; set; }

        [StringLength(50)]
        public string TaxNo { get; set; }

        [InverseProperty(nameof(FavoriteAndBlocked.TargetUser))]
        public virtual ICollection<FavoriteAndBlocked> FavoriteAndBlockedTargetUsers { get; set; }

        [InverseProperty(nameof(FavoriteAndBlocked.User))]
        public virtual ICollection<FavoriteAndBlocked> FavoriteAndBlockedUsers { get; set; }

        [InverseProperty(nameof(Rating.RatingFromNavigation))]
        public virtual ICollection<Rating> RatingRatingFromNavigations { get; set; }

        [InverseProperty(nameof(Rating.RatingToNavigation))]
        public virtual ICollection<Rating> RatingRatingToNavigations { get; set; }

        [InverseProperty(nameof(ServiceRequest.ServiceProvider))]
        public virtual ICollection<ServiceRequest> ServiceRequestServiceProviders { get; set; }

        [InverseProperty(nameof(ServiceRequest.User))]
        public virtual ICollection<ServiceRequest> ServiceRequestUsers { get; set; }

        [InverseProperty(nameof(UserAddress.User))]
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
