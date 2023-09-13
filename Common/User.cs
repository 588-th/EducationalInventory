using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class User
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Login { get; private set; }
        [Required]
        public string Password { get; private set; }
        public string Role { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        [Required]
        public string SecondName { get; private set; }
        public string MiddleName { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
    }
}