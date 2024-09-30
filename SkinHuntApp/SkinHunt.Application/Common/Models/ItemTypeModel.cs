using SkinHunt.Application.Common.Enums;
using System.Text.Json.Serialization;

namespace SkinHunt.Application.Common.Models
{
    public class ItemTypeModel
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Category Category { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Subcategory Subcategory { get; set; }
    }
}
