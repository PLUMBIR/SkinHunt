namespace SkinHunt.Application.Common.Models;

public class UserDto
{
    public string Id { get; set; }
    
    public string Email { get; set; }
    
    public string UserName { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public double Balance { get; set; }
    
    public string Avatar { get; set; }
    
    public string Token { get; set; }

    public string[] Roles { get; set; }
}