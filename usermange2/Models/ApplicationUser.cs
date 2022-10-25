using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace usermange2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(100)]

        public string Firsrname { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        public byte[] profilepicture { get; set; }
    } 
}
