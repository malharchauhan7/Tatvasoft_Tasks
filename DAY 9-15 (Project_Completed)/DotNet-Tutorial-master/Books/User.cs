using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        public string Uid { get; set; }
        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public string UserImage { get; set; } = "";
        public string UserFullName { get; set; }
        public ICollection<ForgotPassword> ForgotPasswords { get; set; }
        public ICollection<UserDetail> UserDetails { get; set; }
    }

    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmployeeId { get; set; }
        public string Manager { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string MyProfile { get; set; }
        public string WhyIVolunteer { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Availability { get; set; }
        public string LinkdInUrl { get; set; }
        public string MySkills { get; set; }
        public string UserImage { get; set; }
        public bool Status { get; set; }
        public User User { get; set; } // Navigation property
    }

    public class ForgotPassword
    {
        public int TempId { get; set; }
        public string Id { get; set; }
        public int UserId { get; set; }
        public DateTime RequestDateTime { get; set; }
        [NotMapped]
        public string EmailAddress { get; set; }
        [NotMapped]
        public string baseUrl { get; set; }
        public User User { get; set; } // Navigation property
    }

    public class ChangePassword
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ResetPassword
    {
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
        public ResetPassword() // Parameterless constructor
        {
        }
    }

    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string UserType { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

public class UpdateUserViewModel
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
    [Required]
    public string UserType { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
