namespace BookStore.Domain.Entities
{
    public class Identifiable
    {
        public Guid Id { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
