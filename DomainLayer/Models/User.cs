using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DomainLayer.Models
{
    /// <summary>
    /// Abstract class for creating users;
    /// </summary>
    [Index(nameof(User.Email), IsUnique = true)]
    public abstract class User
    {
        /// <summary>
        /// User's unique identifier
        /// </summary>
        [Column("UserID")]
        [Key]
        public Guid UserID { get; set; }

        /// <summary>
        /// User's Name
        /// </summary>
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is a required field.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Username must be between 1 and 100 characters long.")]
        public string? Username { get; set; }

        /// <summary>
        /// User's Email address
        /// </summary>
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is a required field.")]
        [PasswordPropertyText(true)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%#*?&])[A-Za-z\d@$!%*#?&]{8,}$",
            ErrorMessage = "Password must have a minimum of eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string? Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please confirm the password.")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        [PasswordPropertyText(true)]
        public string? ConfirmPassword { get; set; }

        /// <summary>
        /// User's Age
        /// </summary>
        [Display(Name = "Age")]
        [Required(ErrorMessage = "Age is a required field.")]
        [Range(10, 200, ErrorMessage = "User must be between least 10 and 200 years old.")]
        public int Age { get; set; }

        /// <summary>
        /// User's Avatar image represented in a byte array
        /// </summary>
        [StringLength(500, MinimumLength = 1, ErrorMessage = "User Avatar URL must be between 1 and 500 characters long.")]
        public string Avatar { get; set; }

        /// <summary>
        /// Flag that checks if the User is logged in or not.
        /// </summary>
        [Required]
        public bool IsActive { get; set; }
    }
}
