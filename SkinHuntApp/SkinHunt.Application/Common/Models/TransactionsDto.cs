namespace SkinHunt.Application.Common.Models
{
    public class TransactionsDto
    {
        public UserDto User { get; set; }

        public SkinDto Skin { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
