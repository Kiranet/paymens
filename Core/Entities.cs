using System;

namespace Core
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }

    public class Payment : BaseEntity
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
        public double Amount { get; set; }
        public string UserId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class Category : BaseEntity
    {
        public Category(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}