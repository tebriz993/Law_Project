using System.ComponentModel.DataAnnotations;

namespace LawProject.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        public string UsernameOrEmail { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        public bool IsRemember { get; set; }

        public LoginVM()
        {
            
        }
    }
}
