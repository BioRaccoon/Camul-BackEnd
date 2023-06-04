using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Dtos
{
    public class ClientDto
    {
        public Guid UserID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public int Age { get; set; }
        public string Avatar { get; set; }
        public string? Incapacity { get; set; }
        public Guid UserLocationID { get; set; }
        public bool IsActive { get; set; }
    }

    public class ClientCreationDto : ClientAddAndUpdateDto { }

    public class ClientUpdateDto : ClientAddAndUpdateDto { }

    public abstract class ClientAddAndUpdateDto
    {

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is a required field.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Username must be between 1 and 100 characters long.")]
        public string? Username { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

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

        [Display(Name = "Age")]
        [Required(ErrorMessage = "Age is a required field.")]
        [Range(10, 200, ErrorMessage = "User must be between least 10 and 200 years old.")]
        public int Age { get; set; }

        public string Avatar { get; set; }

        [Display(Name = "Incapacities")]
        public string? Incapacity { get; set; }

        public Guid UserLocationID { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}
