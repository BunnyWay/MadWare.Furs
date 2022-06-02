using MadWare.Furs.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MadWare.Furs.Models.Invoice
{
    /// <summary>
    /// Vpiše se vrednost osnov po vrsti davka ali dajatve, razdeljeno po davčnih stopnjah, in pripadajoči davek ali dajatev, vrednost dobav na podlagi posebnih ureditev, dobav pri katerih je plačnik DDV kupec blaga ali naročnik storitve, oproščenih dobav in neobdavčljivih dobav, ločeno po davčnih številkah davčnih zavezancev. / The value is entered for bases according to types of taxes or duties, separated per tax rates, and associated taxes or duties, value of supplies on the basis of special arrangements, supplies where the payer of VAT is the buyer of goods or party ordering services, exempt supplies and non-taxable supplies, separated according to tax numbers of taxpayers.
    /// </summary>
    public class TaxesPerSeller : BaseModel
    {
        /// <summary>
        /// Vpiše se davčna številka davčnega zavezanca, v imenu in za račun katerega je bil izdan račun, če je račun izdan v tujem imenu in za tuj račun oziroma če je račun izdal prejemnik računa v imenu in za račun dobavitelja. Če račun ni bil izdan v tujem imenu in za tuj račun, se podatek ne vpisuje. / The tax number of the taxpayer is entered in the name of and on behalf of whose the invoice has been issued, if the invoice has been issued in the name of and on behalf of another person or if the invoice has been issued by the recipient of the invoice in the name of and on behalf of the supplier. If the invoice has not been issued in the name of and on behalf of another person, the data is not entered.
        /// </summary>
        [StringLength(8, MinimumLength = 8)]
        public string SellerTaxNumber { get; set; }

        /// <summary>
        /// Vpišejo se podatki o DDV. / Data about VAT are entered.
        /// Podatek se posreduje le, če račun vsebuje znesek obračunanega DDV. / The data is submitted only if the invoice includes the amount of VAT settled.
        /// Podatek je sestavljen iz davčne stopnje, davčne osnove in zneska davka. / The data consists of the tax rate, tax base and amount of tax.
        /// Za davčne stopnje lahko obstaja seznam davčnih stopenj pri davčnemu organu. / The tax authority may have a list of tax rates.
        /// </summary>
        [XmlElement("VAT")]
        public List<VAT> VAT { get; set; }

        /// <summary>
        /// Vpišejo se podatki o pavšalnem nadomestilu. / Data about the flat-rate compensation are entered.
        /// Podatek se posreduje le, če račun vsebuje znesek obračunanega pavšalnega nadomestila. / The data is submitted only if the invoice includes the amount of the flat-rate compensation settled.
        /// Podatek je sestavljen iz stopnje, osnove in zneska pavšalnega nadomestila. / The data consists of the rate, base and amount of the flat-rate compensation.
        /// </summary>
        [XmlElement("FlatRateCompensation")]
        public List<FlatRateCompensation> FlatRateCompensation { get; set; }

        /// <summary>
        /// Vpiše se skupni znesek ostalih davkov oziroma dajatev (razen DDV), ki so na računu. / The total amount is entered of other taxes or duties (except VAT), which are on the invoice.
        /// Podatek se vpisuje le, če račun vsebuje davke oziroma dajatve, ki niso DDV. / The data is entered only if the invoice includes taxes or duties, which are not VAT.
        /// Decimalno ločilo je pika. / The decimal separator is a dot.
        /// </summary>
        [DecimalPlacesValidation(RequiredDecimalPlaces = 2)]
        public decimal? OtherTaxesAmount { get; set; }

        /// <summary>
        /// Skupna vrednost dobav blaga ali storitev na računu, ki so v skladu z Zakonom o davku na dodano vrednost oproščene plačila DDV (po zmanjšanju za popuste). / The total value of supplies of goods or services on the invoice, which are in accordance with the Value Added Tax Act exempt from VAT payment (after reduction for discounts).
        /// Podatek se vpiše le, če na računu obstaja znesek oproščenih dobav. / The data is entered only if the amount of exempt supplies exists on the invoice.
        /// Decimalno ločilo je pika. / The decimal separator is a dot.
        /// </summary>
        [DecimalPlacesValidation(RequiredDecimalPlaces = 2)]
        public decimal? ExemptVATTaxableAmount { get; set; }

        /// <summary>
        /// Vrednost dobav, za katere je v skladu s 76.a členom Zakona o davku na dodano vrednost prejemnik blaga ali storitev plačnik DDV - obrnjena davčna obveznost (po zmanjšanju za popuste). / The value of supplies, for which in accordance with Article 76.a of the Value Added Tax Act the recipient of goods or services is the payer of VAT – reverse charge procedure (after reduction for discounts).
        /// Podatek se vpiše le, če na računu obstajajo takšne dobave. / The data is entered only if such supplies exist on the invoice.
        /// Decimalno ločilo je pika. / The decimal separator is a dot.
        /// </summary>
        [DecimalPlacesValidation(RequiredDecimalPlaces = 2)]
        public decimal? ReverseVATTaxableAmount { get; set; }

        /// <summary>
        /// Vrednost neobdavčljivih dobav blaga ali storitev na računu (po zmanjšanju za popuste). Podatek se vpiše le, če na računu obstaja vrednost dobav, ki v skladu z Zakonom o davku na dodano vrednost niso predmet DDV. / The value of non-taxable supplies of goods or services on the invoice (after reduction for discounts). The data is entered only if the value of supplies, which are not subject to VAT in accordance with the Value Added Tax Act, exists on the invoice.
        /// Decimalno ločilo je pika. / The decimal separator is a dot.
        /// </summary>
        [DecimalPlacesValidation(RequiredDecimalPlaces = 2)]
        public decimal? NontaxableAmount { get; set; }

        /// <summary>
        /// Vrednost dobav, za katere se uporablja posebna ureditev, po kateri se obdavčuje razlika v ceni, in sicer posebna ureditev za rabljeno blago, umetniške predmete, zbirke in starine (101. člen Zakona o davku na dodano vrednost). Vpiše se tudi vrednost dobav, za katere se obračunava in plačuje DDV po posebni ureditvi za potovalne agencije (97. člen Zakona o davku na dodano vrednost). Vpiše se znesek, ki je zmanjšan za popuste. / The value of supplies, for which a special arrangement is used, on the basis of which the margin is taxed, i.e. the special arrangement for second-hand goods, works of art, collector’s items and antiques (Article 101 of the Value Added Tax Act). The value of supplies is also entered, for which VAT is charged and paid according to the special arrangement for travel agencies (Article 97 of the Value Added Tax Act). The amount is entered, which is reduced for discounts.
        /// Decimalno ločilo je pika. / The decimal separator is a dot.
        /// </summary>
        [DecimalPlacesValidation(RequiredDecimalPlaces = 2)]
        public decimal? SpecialTaxRulesAmount { get; set; }

        public bool ShouldSerializeSellerTaxNumber()
        {
            return !string.IsNullOrEmpty(this.SellerTaxNumber);
        }

        public bool ShouldSerializeOtherTaxesAmount()
        {
            return this.OtherTaxesAmount.HasValue;
        }

        public bool ShouldSerializeFlatRateCompensation()
        {
            return this.FlatRateCompensation != null;
        }

        public bool ShouldSerializeVAT()
        {
            return this.VAT != null;
        }

        public bool ShouldSerializeSpecialTaxRulesAmount()
        {
            return this.SpecialTaxRulesAmount.HasValue;
        }

        public bool ShouldSerializeNontaxableAmount()
        {
            return this.NontaxableAmount.HasValue;
        }

        public bool ShouldSerializeReverseVATTaxableAmount()
        {
            return this.ReverseVATTaxableAmount.HasValue;
        }

        public bool ShouldSerializeExemptVATTaxableAmount()
        {
            return this.ExemptVATTaxableAmount.HasValue;
        }

        public override void Validate()
        {
            base.Validate();

            if (this.VAT != null)
                foreach (var v in this.VAT)
                    v.Validate();

            if (this.FlatRateCompensation != null)
                foreach (var frc in this.FlatRateCompensation)
                    frc.Validate();
        }
    }
}