namespace SkinHunt.Application.Common.Models;

public class SkinDto
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    public ItemTypeDto Type { get; set; }

    public DateTime ReleaseDate { get; set; }

    public double Float { get; set; }

    public string Color { get; set; }

    public string Rarity { get; set; }

    public decimal Price { get; set; }     

    public bool IsDiscount { get; set; }

    public decimal PriceWithDiscount { get; set; }

    public string Photo { get; set; }
}