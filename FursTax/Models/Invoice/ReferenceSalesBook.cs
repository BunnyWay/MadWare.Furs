using System;
using System.ComponentModel.DataAnnotations;

namespace MadWare.Furs.Models.Invoice
{
    /// <summary>
    /// Vpiše se številka izdanega računa iz vezane knjige računov, ki se spreminja, če je bil prvoten račun izdan iz vezane knjige računov. / The number is entered of the issued invoice from the pre-numbered invoice book, which is changed, if the original invoice has been issued from the pre-numbered invoice book.
    /// Podatek se vpiše v primeru, če se z računom, izdanim preko elektronske naprave, spreminja prvoten račun, ki je bil izdan iz vezane knjige računov. / The data is entered in cases when the original invoice is changed, which has been issued from the pre-numbered invoice book, with the invoice, issued via the electronic device.
    /// </summary>
    public class ReferenceSalesBook : BaseModel
    {
        /// <summary>
        /// Vpiše se številka izdanega računa iz vezane knjige računov, ki se spreminja, če je bil prvoten račun izdan iz vezane knjige računov. / The number is entered of the issued invoice from the pre-numbered invoice book, which is changed, if the original invoice has been issued from the pre-numbered invoice book.
        /// Podatek se vpiše v primeru, če se z računom, izdanim preko elektronske naprave, spreminja prvoten račun, ki je bil izdan iz vezane knjige računov. / The data is entered in cases when the original invoice is changed, which has been issued from the pre-numbered invoice book, with the invoice, issued via the electronic device.
        /// </summary>
        [Required]
        public SalesBookIdentifier ReferenceSalesBookIdentifier { get; set; }

        /// <summary>
        /// Vpiše se datum izdaje prvotnega računa iz vezane knjige računov, ki se spreminja, če je bil prvoten račun izdan iz vezane knjige računov. / The issuing date is entered for the original invoice from the pre-numbered invoice book, which is changed if the original invoice has been issued from the pre-numbered invoice book.
        /// Podatek se vpiše v primeru, če se z računom, izdanim preko elektronske naprave, spreminja prvoten račun, ki je bil izdan iz vezane knjige računov. / The data is entered in cases when the original invoice, which has been issued from the pre-numbered invoice book, is changed with the invoice, issued via the electronic device.
        /// </summary>
        public DateTime? ReferenceSalesBookIssueDate { get; set; }

        public bool ShouldSerializeReferenceSalesBookIssueDate()
        {
            return this.ReferenceSalesBookIssueDate.HasValue;
        }

        public override void Validate()
        {
            base.Validate();

            this.ReferenceSalesBookIdentifier.Validate();
        }
    }
}