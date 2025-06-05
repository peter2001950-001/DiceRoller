using System.ComponentModel.DataAnnotations;

namespace DiceRoller.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }
        
    }
}
