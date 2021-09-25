using System.ComponentModel.DataAnnotations;

namespace WorldCities.Models.Dto
{
    public class CityForUpdateDto
    {
        [Required]
        public string Name { get; set; }

        public string Name_ASCII { get; set; }

        [Required]
        public decimal Lat { get; set; }

        [Required]
        public decimal Lon { get; set; }
    }
}
