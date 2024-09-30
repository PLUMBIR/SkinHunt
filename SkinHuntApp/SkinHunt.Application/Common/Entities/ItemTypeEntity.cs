using SkinHunt.Application.Common.Enums;

namespace SkinHunt.Application.Common.Entities
{
    public class ItemTypeEntity : BaseEntity
    {
        public Category Category { get; set; }

        public Subcategory Subcategory { get; set; }
    }
}
