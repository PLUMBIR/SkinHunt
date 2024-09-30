namespace SkinHunt.Application.Common.Models;

public class BasketDto
{
    public string Id { get; set; }
    
    public UserDto User { get; set; }

    public SkinDto Skin { get; set; }

    public DateTime CreatedDate {  get; set; }
}