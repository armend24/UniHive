using System.ComponentModel.DataAnnotations;

namespace UniHive.Models
{
    public class UserViewModel
    {
        public int? UserID { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string FullName { get; set; }


        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string Role { get; set; }

        public bool IsActive { get; set; }
    }
}
