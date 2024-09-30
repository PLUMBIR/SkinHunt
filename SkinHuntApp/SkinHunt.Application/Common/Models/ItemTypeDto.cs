using System.Text.Json.Serialization;
using SkinHunt.Application.Common.Enums;

namespace SkinHunt.Application.Common.Models;

public class ItemTypeDto
{
    public string Id { get; set; }
    
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public Category Category { get; set; }

    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public Subcategory Subcategory { get; set; }
}