using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WorldCities.Models.Dto
{
    public class CountryForUpdateDto
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Country code (in ISO 3166-1 ALPHA-2 format)
        /// </summary>
        [Required]
        [StringLength(150)]
        [JsonPropertyName("iso2")]
        public string ISO2 { get; set; }

        /// <summary>
        /// Country code (in ISO 3166-1 ALPHA-3 format)
        /// </summary>
        [Required]
        [StringLength(150)]
        [JsonPropertyName("iso3")]
        public string ISO3 { get; set; }
    }
}
