using System.ComponentModel.DataAnnotations;

namespace OptimunLegalServices.ViewModel
{
    public class LoginVM
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
