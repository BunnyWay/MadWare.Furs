using System;
using System.ComponentModel.DataAnnotations;

namespace MadWare.Furs.Models.Invoice
{
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
    public class ReferenceInvoice : BaseModel
    {
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
        [Required]
        public InvoiceIdentifier ReferenceInvoiceIdentifier { get; set; }

        /// <summary>
        /// Vpiše se datum in čas izdaje prvotnega računa v primeru naknadne spremembe podatkov na prvotnem računu, če je bil prvoten račun izdan preko elektronske naprave. / Date and time are entered of issuing the original invoice in cases of subsequent changes of data on the original invoice if the original invoice has been issued via the electronic device.
        /// Podatek se vpiše v primeru, če se z računom, izdanim preko elektronske naprave, spreminja prvoten račun, ki je bil izdan preko elektronske naprave. / The data is entered in cases if the original invoice, which has been issued via the electronic device, is changed with the invoice, issued via the electronic device.
        /// </summary>
        public DateTime? ReferenceInvoiceIssueDateTime { get; set; }

        public bool ShouldSerializeReferenceInvoiceIssueDateTime()
        {
            return this.ReferenceInvoiceIssueDateTime.HasValue;
        }

        public override void Validate()
        {
            base.Validate();

            this.ReferenceInvoiceIdentifier.Validate();
        }
    }
}