using System.ComponentModel.DataAnnotations;

namespace MadWare.Furs.Models.BusinessPremise
{
    public class Address : BaseModel
    {
        /// <summary>
        /// Ulica / Street
        /// </summary>
        [Required(), StringLength(100, MinimumLength = 1)]
        public string Street { get; set; }

        /// <summary>
        /// Hišna številka / House number
        /// </summary>
        [Required(), StringLength(10, MinimumLength = 1)]
        public string HouseNumber { get; set; }

        /// <summary>
        /// Dodatek k hišni številki / Addition to the house number
        /// </summary>
        [StringLength(10, MinimumLength = 1)]
        public string HouseNumberAdditional { get; set; }

        /// <summary>
        /// Naselje / Town
        /// </summary>
        [Required(), StringLength(100, MinimumLength = 1)]
        public string Community { get; set; }

        /// <summary>
        /// Pošta / Post office
        /// </summary>
        [Required(), StringLength(40, MinimumLength = 1)]
        public string City { get; set; }

        /// <summary>
        /// Poštna številka / Postcode
        /// </summary>
        [Required(), StringLength(4, MinimumLength = 4)]
        public string PostalCode { get; set; }

        public bool ShouldSerializeHouseNumberAdditional()
        {
            return !string.IsNullOrEmpty(this.HouseNumberAdditional);
        }
    }
}