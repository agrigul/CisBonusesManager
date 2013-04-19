using System.ComponentModel.DataAnnotations;

namespace Web.Models.ValueObjects
{
//    public class UsersContext : DbContext
//    {
//        public UsersContext()
//            : base("DefaultConnection")
//        {
//        }
//
//        public DbSet<UserProfile> UserProfiles { get; set; }
//    }

//    [Table("UserProfile")]
//    public class UserProfile
//    {
//        [Key]
//        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
//        public int UserId { get; set; }
//        public string UserName { get; set; }
//    }

//    public class RegisterExternalLoginModel
//    {
//        [Required]
//        [Display(Name = "User name")]
//        public string UserName { get; set; }
//
//        public string ExternalLoginData { get; set; }
//    }

//    public class LocalPasswordModel
//    {
//        [Required]
//        [DataType(DataType.Password)]
//        [Display(Name = "Current password")]
//        public string OldPassword { get; set; }
//
//        [Required]
//        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
//        [DataType(DataType.Password)]
//        [Display(Name = "New password")]
//        public string NewPassword { get; set; }
//
//        [DataType(DataType.Password)]
//        [Display(Name = "Confirm new password")]
//        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
//        public string ConfirmPassword { get; set; }
//    }

    /// <summary>
    /// Class of user's credentials
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remember me]; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginModel"/> class.
        /// </summary>
        public  LoginModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginModel"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="passwrod">The passwrod.</param>
        public LoginModel(string userName, string passwrod)
        {
            UserName = userName;
            Password = passwrod;
        }
    }

//    public class RegisterModel
//    {
//        [Required]
//        [Display(Name = "User name")]
//        public string UserName { get; set; }
//
//        [Required]
//        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
//        [DataType(DataType.Password)]
//        [Display(Name = "Password")]
//        public string Password { get; set; }
//
//        [DataType(DataType.Password)]
//        [Display(Name = "Confirm password")]
//        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
//        public string ConfirmPassword { get; set; }
//    }
//
//    public class ExternalLogin
//    {
//        public string Provider { get; set; }
//        public string ProviderDisplayName { get; set; }
//        public string ProviderUserId { get; set; }
//    }
}
