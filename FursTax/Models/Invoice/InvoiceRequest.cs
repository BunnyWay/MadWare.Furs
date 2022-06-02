using MadWare.Furs.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MadWare.Furs.Models.Invoice
{
    public class InvoiceRequest : BaseModel
    {
        [XmlAttribute()]
        public string Id = "data";

        /// <summary>
        /// Glava sporočila / Message header
        /// </summary>
        [Required]
        public Header Header { get; set; }

        /// <summary>
        /// Račun / Invoice
        /// </summary>
        public Invoice Invoice { get; set; }

        /// <summary>
        /// Račun iz vezane knjige / Sales book invoice
        /// </summary>
        public SalesBookInvoice SalesBookInvoice { get; set; }

        public InvoiceRequest()
        {
            this.Header = new Header();
        }

        public bool ShouldSerializeInvoice()
        {
            return this.Invoice != null;
        }

        public bool ShouldSerializeSalesBookInvoice()
        {
            return this.SalesBookInvoice != null;
        }

        public override void Validate()
        {
            base.Validate();

            this.Header.Validate();

            if (this.Invoice != null && this.SalesBookInvoice != null)
                throw new Exception("Invoice and SalesBookInvoice can not be both included in InvoiceRequest.");

            if (this.Invoice == null && this.SalesBookInvoice == null)
                throw new Exception("Invoice and SalesBookInvoice can not be both be null in InvoiceRequest.");

            if (this.Invoice != null)
                this.Invoice.Validate();

            if (this.SalesBookInvoice != null)
                this.SalesBookInvoice.Validate();
        }
    }
}