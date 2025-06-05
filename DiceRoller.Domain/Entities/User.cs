using System.ComponentModel.DataAnnotations;

namespace DiceRoller.Domain.Entities
{
    public class User : BaseEntity
    {

        [MaxLength(200)]
        public string FirstName { get; set; }

        [MaxLength(200)]
        public string LastName { get; set; }

        [MaxLength(300)]
        public string Email { get; set; }

        [MaxLength(300)]
        public string PasswordHash { get; set; }

        public string Avatar { get; set; }
    }
}
