using MadWare.Furs.Validation;
using System.ComponentModel.DataAnnotations;

namespace MadWare.Furs.Models.Invoice
{
    /// <summary>
    /// Vpišejo se podatki o DDV. / Data about VAT are entered.
    /// Podatek se posreduje le, če račun vsebuje znesek obračunanega DDV. / The data is submitted only if the invoice includes the amount of VAT settled.
    /// Podatek je sestavljen iz davčne stopnje, davčne osnove in zneska davka. / The data consists of the tax rate, tax base and amount of tax.
    /// Za davčne stopnje lahko obstaja seznam davčnih stopenj pri davčnemu organu. / The tax authority may have a list of tax rates.
    /// </summary>
    public class VAT : BaseModel
    {
        /// <summary>
        /// Vrednost davčne stopnje. / Value of the tax rate
        /// Decimalno ločilo je pika. / The decimal separator is a dot.
        /// </summary>
        [Required, DecimalPlacesValidation(RequiredDecimalPlaces = 2)]
        public decimal? TaxRate { get; set; }

        /// <summary>
        /// Znesek davčne osnove (po zmanjšanju za popuste). / Amount of the tax base (after reduction for discounts)
        /// Decimalno ločilo je pika. / The decimal separator is a dot.
        /// </summary>
        [Required, DecimalPlacesValidation(RequiredDecimalPlaces = 2)]
        public decimal? TaxableAmount { get; set; }

        /// <summary>
        /// Znesek davka. / Amount of tax
        /// Decimalno ločilo je pika. / The decimal separator is a dot.
        /// </summary>
        [Required, DecimalPlacesValidation(RequiredDecimalPlaces = 2)]
        public decimal? TaxAmount { get; set; }
    }
}