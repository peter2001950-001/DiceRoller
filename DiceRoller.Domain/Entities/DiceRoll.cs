namespace DiceRoller.Domain.Entities
{
    public class DiceRoll : BaseEntity
    {
        public int DiceRoll1 { get; set; }

        public int DiceRoll2 { get; set; }

        public User? User { get; set; }
    }
}
