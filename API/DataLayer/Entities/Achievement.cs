namespace DataLayer.Entities
{
    public class Achievement : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
