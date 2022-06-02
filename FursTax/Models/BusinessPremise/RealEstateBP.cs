using System.ComponentModel.DataAnnotations;

namespace MadWare.Furs.Models.BusinessPremise
{
    public class RealEstateBP : BaseModel
    {
        /// <summary>
        /// Vpiše se identifikacijska oznaka stavbe oziroma dela stavbe, kjer se nahaja poslovni prostor, kot je določena v registru nepremičnin. Oznaka se vpiše, če zavezanec izdaja račune v nepremičnem poslovnem prostoru. / The identification mark of the building or part of the building is entered, where business premises are located, as it is defined in the register of immovable property. The mark is entered if the person liable issues invoices in immovable business premises.
        /// Identifikacijska oznaka nepremičnine je sestavljena iz treh delov / The identification mark of the immovable property consists of three parts:
        /// - Številka katastrske občine / Number of the cadastral community
        /// - Številka stavbe / Number of the building
        /// - Številka dela stavbe / Number of the part of the building
        /// Podatki se vpisujejo ločeno. / Data are entered separately.
        /// </summary>
        [Required]
        public PropertyID PropertyID { get; set; }

        /// <summary>
        /// Vpiše se naslov poslovnega prostora, če zavezanec izdaja račune v nepremičnem poslovnem prostoru. / The address of business premises is entered if the person liable issues invoices in immovable business premises.
        /// Naslov sestavljajo ulica in hišna številka, dodatek k hišni številki, naselje, pošta in poštna številka. / The address includes: street, house number, addition to the house number, town, post office and postcode.
        /// </summary>
        [Required]
        public Address Address { get; set; }

        public override void Validate()
        {
            base.Validate();

            this.PropertyID.Validate();
            this.Address.Validate();
        }
    }
}