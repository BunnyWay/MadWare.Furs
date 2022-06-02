using System.ComponentModel.DataAnnotations;

namespace MadWare.Furs.Models.BusinessPremise
{
    public class SoftwareSupplier : BaseModel
    {
        /// <summary>
        /// Vpiše se davčna številka pravne ali fizične osebe – proizvajalca ali vzdrževalca programske opreme s sedežem v Sloveniji. / The tax number is entered of a legal entity or an individual – producer or software maintenance provider established in Slovenia.
        /// </summary>
        [StringLength(8, MinimumLength = 8)]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Vpiše se naziv in naslov proizvajalca ali vzdrževalca programske opreme, ki nima sedeža v Sloveniji. / The title and address is entered of the producer or software maintenance provider not established in Slovenia.
        /// </summary>
        [StringLength(1000, MinimumLength = 1)]
        public string NameForeign { get; set; }

        public bool ShouldSerializeTaxNumber()
        {
            return !string.IsNullOrEmpty(this.TaxNumber);
        }

        public bool ShouldSerializeNameForeign()
        {
            return !string.IsNullOrEmpty(this.NameForeign);
        }
    }
}