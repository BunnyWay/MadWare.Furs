using MadWare.Furs.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MadWare.Furs.Models.Invoice
{
    public class SalesBookInvoice : BaseModel
    {
        /// <summary>
        /// Vpiše se davčna številka zavezanca, ki je izdal račun. / The tax number of the person liable, who has issued the invoice, is entered.
        /// </summary>
        [Required(), StringLength(8, MinimumLength = 8)]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Vpiše se datum in čas izdaje računa, ki je naveden na računu. / Date and time of issuing the invoice, which is stated on the invoice, are entered.
        /// </summary>
        [XmlIgnore]
        [Required]
        public DateTime? IssueDate { get; set; }

        [XmlElement("IssueDate")]
        public string IssueDateFormatted
        {
            get
            {
                return this.IssueDate.Value.ToUniversalTime().ToString("yyyy-MM-dd");
            }
            set { }
        }

        [Required]
        public SalesBookIdentifier SalesBookIdentifier { get; set; }

        /// <summary>
        /// Vpiše se oznaka poslovnega prostora v katerem je bil izdan račun iz vezane knjige računov. / The mark of business premises is entered, in which the invoice has been issued from the pre-numbered invoice book.
        /// Oznaka poslovnega prostora mora biti enaka oznaki, ki je bila posredovana v okviru podatkov o poslovnih prostorih. / The mark of business premises shall be the same as the mark, which has been submitted within data about business premises.
        /// Vsebuje lahko samo črke in številke / It may include only the following letters and numbers: 0-9, a-z, A-Z.
        /// </summary>
        [Required(), StringLength(20, MinimumLength = 1), RegularExpression("^[0-9a-zA-Z]*$")]
        public string BusinessPremiseID { get; set; }

        /// <summary>
        /// Vpiše se davčna številka oziroma identifikacijska številka za namene DDV kupca oziroma naročnika v primeru, ko so ti podatki v skladu z davčnimi predpisi navedeni na računu.
        /// Podatek se vpiše, če je naveden na računu. / The tax number is entered or identification number for VAT purposes of the buyer or ordering party in cases when these data are stated on the invoice in accordance with tax regulations.
        /// </summary>
        [StringLength(20, MinimumLength = 1)]
        public string CustomerVATNumber { get; set; }

        /// <summary>
        /// Vnese se skupni znesek računa. Vpiše se znesek računa skupaj z DDV in ostalimi davki/dajatvami, zmanjšan za zneske popustov. / The total amount of the invoice is entered. The amount of the invoice is entered together with VAT and other taxes/duties, decreased for amounts of discounts.
        /// Decimalno ločilo je pika. / The decimal separator is a dot.
        /// </summary>
        [Required, DecimalPlacesValidation(RequiredDecimalPlaces = 2)]
        public decimal? InvoiceAmount { get; set; }

        /// <summary>
        /// Vpiše se znesek povračil na računu, ki se priznajo kupcu (npr. na podlagi dobropisa za vračilo embalaže). / The amount of refunds on the invoice, which are recognized to the buyer (e.g. on the basis of credit for returning packaging), is entered.
        /// Podatek se vpiše le, če na računu obstajajo povračila. / The data is entered only if there are refunds on the invoice.
        /// Decimalno ločilo je pika. / The decimal separator is a dot.
        /// </summary>
        [DecimalPlacesValidation(RequiredDecimalPlaces = 2)]
        public decimal? ReturnsAmount { get; set; }

        /// <summary>
        /// Vpiše se znesek računa za plačilo. / The amount of the invoice for payment is entered.
        /// Decimalno ločilo je pika. / The decimal separator is a dot.
        /// </summary>
        [Required, DecimalPlacesValidation(RequiredDecimalPlaces = 2)]
        public decimal? PaymentAmount { get; set; }

        /// <summary>
        /// Vpiše se vrednost osnov po vrsti davka ali dajatve, razdeljeno po davčnih stopnjah, in pripadajoči davek ali dajatev, vrednost dobav na podlagi posebnih ureditev, dobav pri katerih je plačnik DDV kupec blaga ali naročnik storitve, oproščenih dobav in neobdavčljivih dobav, ločeno po davčnih številkah davčnih zavezancev. / The value is entered for bases according to types of taxes or duties, separated per tax rates, and associated taxes or duties, value of supplies on the basis of special arrangements, supplies where the payer of VAT is the buyer of goods or party ordering services, exempt supplies and non-taxable supplies, separated according to tax numbers of taxpayers.
        /// </summary>
        [XmlElement("TaxesPerSeller")]
        [Required]
        public List<TaxesPerSeller> TaxesPerSeller { get; set; }

        /// <summary>
        /// Vpiše se številka prvotnega računa v primeru naknadne spremembe podatkov na prvotnem računu, če je bil prvoten račun izdan preko elektronske naprave. / The number of the original invoice is entered in cases of subsequent changes of data on the original invoice if the original invoice has been issued via the electronic device.
        /// Zavezanec izvaja postopek potrjevanja računov tudi za vse naknadne spremembe podatkov na računu, ki spreminjajo prvoten račun in se nanj nedvoumno nanašajo. / The person liable conducts the procedure for verification of invoices also for all subsequent changes of data on the invoice, which change the original invoice and they refer to it with reasonable certainty.
        /// Podatek se vpiše v primeru, če se z računom, izdanim preko elektronske naprave, spreminja prvoten račun, ki je bil izdan preko elektronske naprave. / The data is entered in cases if the original invoice, which has been issued via the electronic device, changes with the invoice, issued via the electronic device.
        /// Za vpis številke računa, ki se spreminja, veljajo enaka pravila kot pri vpisu številke računa.Številka računa je sestavljena iz treh delov / Rules for entry of the invoice number, which is changed, are the same as those for entry of the invoice number.The invoice number includes three parts:
        /// - Oznaka poslovnega prostora / Mark of business premises
        /// - Oznaka elektronske naprave za izdajanje računov / Mark of the electronic device for issuing invoices
        /// - Zaporedna številka računa / Sequence number of the invoice
        /// Številka računa se na računu navede v naslednji obliki / The invoice number is stated on the invoice in the following form:
        /// oznaka poslovnega prostora-oznaka elektronske naprave-zaporedna številka računa / mark of business premises-mark of the electronic device- sequence invoice number
        /// Primer / Example: TRGOVINA1-BLAG2-1234
        /// Podatki se vpisujejo ločeno. / Data are entered separately.
        /// </summary>
        [XmlElement("ReferenceInvoice")]
        public List<ReferenceInvoice> ReferenceInvoice { get; set; }

        /// <summary>
        /// Vpiše se številka izdanega računa iz vezane knjige računov, ki se spreminja, če je bil prvoten račun izdan iz vezane knjige računov. / The number is entered of the issued invoice from the pre-numbered invoice book, which is changed, if the original invoice has been issued from the pre-numbered invoice book.
        /// Podatek se vpiše v primeru, če se z računom, izdanim preko elektronske naprave, spreminja prvoten račun, ki je bil izdan iz vezane knjige računov. / The data is entered in cases when the original invoice is changed, which has been issued from the pre-numbered invoice book, with the invoice, issued via the electronic device.
        /// </summary>
        [XmlElement("ReferenceSalesBook")]
        public List<ReferenceSalesBook> ReferenceSalesBook { get; set; }

        /// <summary>
        /// Vpišejo se morebitne druge oznake, ki podrobneje pojasnjujejo zapise v zvezi z vsebino izdanih računov in njihove spremembe. / Potential other marks are entered, which explain in detail the records in connection with the content of invoices issued and their changes.
        /// </summary>
        public string SpecialNotes { get; set; }

        public bool ShouldSerializeReturnsAmount()
        {
            return this.ReturnsAmount.HasValue;
        }

        public bool ShouldSerializeCustomerVATNumber()
        {
            return !string.IsNullOrEmpty(this.CustomerVATNumber);
        }

        public bool ShouldSerializeReferenceInvoice()
        {
            return this.ReferenceInvoice != null;
        }

        public bool ShouldSerializeReferenceSalesBook()
        {
            return this.ReferenceSalesBook != null;
        }

        public bool ShouldSerializeSpecialNotes()
        {
            return !string.IsNullOrEmpty(this.SpecialNotes);
        }

        public override void Validate()
        {
            base.Validate();

            this.SalesBookIdentifier.Validate();

            foreach (var tps in this.TaxesPerSeller)
                tps.Validate();

            if (this.ReferenceInvoice != null)
                foreach (var ri in this.ReferenceInvoice)
                    ri.Validate();

            if (this.ReferenceSalesBook != null)
                foreach (var rsb in this.ReferenceSalesBook)
                    rsb.Validate();
        }
    }
}