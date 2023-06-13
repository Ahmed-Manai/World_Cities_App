using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Data.Models
{
    [Table("Countries")]
    [Index(nameof(Name))]
    [Index(nameof(ISO2))]
    [Index(nameof(ISO3))]
    public class Country
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
        public string Name { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        public string ISO2 { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        public string ISO3 { get; set; } = null;

        public ICollection<City>? cities { get; set; } = null!;


        #endregion
    }
}

