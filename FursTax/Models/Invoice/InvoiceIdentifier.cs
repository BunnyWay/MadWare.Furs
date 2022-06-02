using System.ComponentModel.DataAnnotations;

namespace MadWare.Furs.Models.Invoice
{
    /// <summary>
    /// Vpiše se številka izdanega računa. / The number of the issued invoice is entered.
    /// Vpiše se tudi številka dokumenta, ki spreminja prvotni račun(dobropis, storno,…) v primeru izvajanja postopka potrjevanja naknadne spremembe podatkov na računu, ki spreminja prvoten račun in se nanj nedvoumno nanaša. / The number of the document is also entered, which changes the original invoice (credit, reversing, etc.) in cases of performing the procedure for verification of subsequent changes of data on the invoice, which changes the original invoice and refers to it with reasonable certainty.
    /// Številka računa je sestavljena iz treh delov / The invoice number includes three parts:
    /// - Oznaka poslovnega prostora / Mark of business premises
    /// - Oznaka elektronske naprave za izdajanje računov / Mark of the electronic device for issuing invoices
    /// - Zaporedna številka računa / Sequence number of the invoice
    /// Številka računa se na računu navede v naslednji obliki / The invoice number is stated on the invoice in the following form:
    /// oznaka poslovnega prostora-oznaka elektronske naprave-zaporedna številka računa / mark of business premises-mark of the electronic device-sequence invoice number
    /// Primer / Example: TRGOVINA1-BLAG2-1234
    /// Podatki se vpisujejo ločeno. / Data are entered separately.
    /// </summary>
    public class InvoiceIdentifier : BaseModel
    {
        /// <summary>
        /// Vsebuje lahko samo črke in številke / It may include only the following letters and numbers: 0-9, a-z, A-Z.
        /// </summary>
        [Required(), StringLength(20, MinimumLength = 1), RegularExpression("^[0-9a-zA-Z]*$")]
        public string BusinessPremiseID { get; set; }

        /// <summary>
        /// Vsebuje lahko samo črke in številke / It may include only the following letters and numbers: 0-9, a-z, A-Z.
        /// </summary>
        [Required(), StringLength(20, MinimumLength = 1), RegularExpression("^[0-9a-zA-Z]*$")]
        public string ElectronicDeviceID { get; set; }

        /// <summary>
        /// Vsebuje lahko samo številke 0-9. Niso dovoljene vodilne ničle. / It may include only numbers 0-9. Zeros cannot be on the first place.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int InvoiceNumber { get; set; }
    }
}