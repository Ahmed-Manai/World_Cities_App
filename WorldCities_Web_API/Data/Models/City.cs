using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Data.Models
{
    [Table("Cities")]
    [Index(nameof(Name))]
    [Index(nameof(Lat))]
    [Index(nameof(Lon))]
    public class City
    {

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(7,4)")]
        public decimal Lat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName="deciaml(7,4)")]
        public decimal Lon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }


        #endregion
        #region Navigation Properties
        public Country? Country { get; set; } = null!;
        #endregion
    }
}
