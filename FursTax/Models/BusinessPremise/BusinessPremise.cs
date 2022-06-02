using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MadWare.Furs.Models.BusinessPremise
{
    public class BusinessPremise : BaseModel
    {
        public enum ClosingTagEnum { Z }

        /// <summary>
        /// Davčna številka zavezanca, ki izdaja račune / Tax number of the person liable, who issues invoices
        /// </summary>
        [Required(), StringLength(8, MinimumLength = 8)]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Vpiše se oznaka poslovnega prostora v katerem zavezanec izdaja račune pri gotovinskem poslovanju. Oznaka je lahko sestavljena iz številk in črk / The mark is entered of business premises, in which the person liable issues invoices in cash operations. The mark may include the following number and letters: / 0-9, a-z, A-Z.
        /// Oznaka mora biti enaka kot tista, ki je navedena na računih. / The mark shall be the same as the mark, stated on invoices.
        /// Oznaka je enkratna na nivoju zavezanca. / The mark is unique at the level of the person liable.
        /// </summary>
        [Required(), StringLength(20, MinimumLength = 1), RegularExpression("^[0-9a-zA-Z]*$")]
        public string BusinessPremiseID { get; set; }

        /// <summary>
        /// Vpišejo se podatki o nepremičnem ali premičnem poslovnem prostoru. / Data are entered about immovable or movable business premises.
        /// </summary>
        public BPIdentifier BPIdentifier { get; set; }

        /// <summary>
        /// Datum začetka veljavnosti podatkov o poslovnem prostoru, ki se posredujejo. / The date when data about business premises, which are submitted, become valid.
        /// </summary>
        [XmlIgnore]
        [Required]
        public DateTime? ValidityDate { get; set; }

        [XmlElement("ValidityDate")]
        public string ValidityDateFormatted
        {
            get
            {
                return this.ValidityDate.Value.ToUniversalTime().ToString("yyyy-MM-dd");
            }
            set { }
        }

        /// <summary>
        /// Vpiše se podatek o zaprtju poslovnega prostora, če gre za trajno zaprtje. / The data is entered about the closure of business premises if the closure is permanent.
        /// Možna je vrednost »Z«. / The possible value is »Z«.
        /// Po zaprtju v tem poslovnem prostoru ni več možno izdajati računov in računov z oznako tega poslovnega prostora ni več možno posredovati davčnemu organu. / After closure issuing invoices is not possible anymore in these business premises and it is not possible to submit invoices to the tax authority with the mark of these business premises.
        /// </summary>
        public ClosingTagEnum? ClosingTag { get; set; }

        /// <summary>
        /// Vpiše se podatek o proizvajalcu ali vzdrževalcu programske opreme za izdajanje računov. / The data is entered about the producer or software maintenance provider for issuing invoices.
        /// Vpiše se eden od podatkov / One of the following data is entered:
        /// - Davčna številka pravne ali fizične osebe - proizvajalca ali vzdrževalca programske opreme s sedežem v Sloveniji in / tax number of a legal entity or an individual – producer or software maintenance provider established in Slovenia and
        /// - naziv in naslov proizvajalca ali vzdrževalca programske opreme, ki nima sedeža v Sloveniji / title and address of the producer or software maintenance provider not established in Slovenia
        /// </summary>
        [Required]
        [XmlElement("SoftwareSupplier")]
        public List<SoftwareSupplier> SoftwareSupplier { get; set; }

        /// <summary>
        /// Vpišejo se morebitne druge oznake, ki podrobneje pojasnjujejo zapise v zvezi z vsebino podatkov o poslovnem prostoru. / Any other potential marks are entered, which explain in detail the records in connection with the content of data about business premises.
        /// </summary>
        public string SpecialNotes { get; set; }

        public bool ShouldSerializeClosingTag()
        {
            return this.ClosingTag.HasValue;
        }

        public bool ShouldSerializeSpecialNotes()
        {
            return !string.IsNullOrEmpty(this.SpecialNotes);
        }

        public override void Validate()
        {
            base.Validate();

            this.BPIdentifier.Validate();

            foreach (var ss in this.SoftwareSupplier)
                ss.Validate();
        }
    }
}