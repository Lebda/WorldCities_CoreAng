using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorldCities.Models.Models
{
    [Table("Countries")]
    public class Country
    {
        #region Constructor
        public Country()
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// The unique id and primary key for this Country
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Country name (in UTF8 format)
        /// </summary>
        [StringLength(150)]
        public string Name { get; set; }
        /// <summary>
        /// Country code (in ISO 3166-1 ALPHA-2 format)
        /// </summary>
        [StringLength(150)]
        [JsonPropertyName("iso2")]
        public string ISO2 { get; set; }
        /// <summary>
        /// Country code (in ISO 3166-1 ALPHA-3 format)
        /// </summary>
        [StringLength(150)]
        [JsonPropertyName("iso3")]
        public string ISO3 { get; set; }
        #endregion

        /// <summary>
        /// A list containing all the cities related to this country.
        /// </summary>
        public virtual List<City> Cities { get; set; }
    }
}
