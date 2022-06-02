using System.ComponentModel.DataAnnotations;

namespace MadWare.Furs.Models.Invoice
{
    public class SalesBookIdentifier : BaseModel
    {
        /// <summary>
        /// Vpiše se številka izdanega računa iz vezane knjige računov, ki se spreminja, če je bil prvoten račun izdan iz vezane knjige računov. / The number is entered of the issued invoice from the pre-numbered invoice book, which is changed, if the original invoice has been issued from the pre-numbered invoice book.
        /// Podatek se vpiše v primeru, če se z računom, izdanim preko elektronske naprave, spreminja prvoten račun, ki je bil izdan iz vezane knjige računov. / The data is entered in cases when the original invoice is changed, which has been issued from the pre-numbered invoice book, with the invoice, issued via the electronic device.
        /// </summary>
        [Required(), StringLength(20, MinimumLength = 1)]
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Vpiše se številka posameznega obrazca računa (seta) izdanega iz vezane knjige računov, ki se spreminja, če je bil prvoten račun izdan iz vezane knjige računov / The number is entered of an individual invoice form (set) issued from the pre-numbered invoice book, which is changed if the original invoice has been issued from the pre-numbered invoice book.
        /// Podatek se vpiše v primeru, če se z računom, izdanim preko elektronske naprave, spreminja prvoten račun, ki je bil izdan iz vezane knjige računov. / The data is entered in cases when the original invoice, which has been issued from the pre-numbered invoice book, is changed with the invoice, issued via the electronic device.
        /// </summary>
        [Required(), StringLength(2, MinimumLength = 2)]
        public string SetNumber { get; set; }

        /// <summary>
        /// Vpiše se serijska številka vezane knjige računov iz katere je bil izdan račun, ki se spreminja, če je bil prvoten račun izdan iz vezane knjige računov. / The serial number is entered of the pre-numbered invoice book, from which the invoice, which is changed, has been issued if the original invoice has been issued from the pre-numbered invoice book.
        /// Podatek se vpiše v primeru, če se z računom, izdanim preko elektronske naprave, spreminja prvoten račun, ki je bil izdan iz vezane knjige računov. / The data is entered in cases when the original invoice, which has been issued from the pre-numbered invoice book, is changed with the invoice, issued via the electronic device.
        /// </summary>
        [Required(), StringLength(12, MinimumLength = 12)]
        public string SerialNumber { get; set; }
    }
}