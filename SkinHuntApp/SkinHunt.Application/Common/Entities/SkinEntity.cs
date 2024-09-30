namespace SkinHunt.Application.Common.Entities
{
    public class SkinEntity : BaseEntity
    {
        public string Name { get; set; }

        public ItemTypeEntity Type { get; set; }

        public DateTime ReleaseDate { get; set; }

        public double Float { get; set; }

        public string Color { get; set; }

        public string Rarity { get; set; }

        public decimal Price { get; set; }     

        public bool IsDiscount { get; set; }

        public decimal PriceWithDiscount { get; set; }

        public string Photo { get; set; }
    }
}
