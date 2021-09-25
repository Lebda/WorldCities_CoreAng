using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WorldCities.Models.Dto
{
    public class CityForCreateDto
    {
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("nameAscii")]
        public string Name_ASCII { get; set; }

        [Required]
        public decimal Lat { get; set; }

        [Required]
        public decimal Lon { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
