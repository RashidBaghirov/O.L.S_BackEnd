using OptimunLegalServices.Utilities;
using System.ComponentModel.DataAnnotations;

namespace OptimunLegalServices.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Required(ErrorMessage = "Confirm password is required.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please accept the registration.")]
        public bool Terms { get; set; }
    }
}
