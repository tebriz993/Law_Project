using Microsoft.AspNetCore.Identity;

namespace LawProject.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Reminder { get; set; }
    }
}
