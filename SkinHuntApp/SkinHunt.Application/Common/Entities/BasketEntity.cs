namespace SkinHunt.Application.Common.Entities
{
    public class BasketEntity : BaseEntity
    {
        public UserEntity User { get; set; }

        public SkinEntity Skin { get; set; }

        public DateTime CreatedDate {  get; set; }
    }
}
