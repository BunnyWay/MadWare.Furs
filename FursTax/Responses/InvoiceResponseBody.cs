using MadWare.Furs.Models.Invoice;
using System.Xml.Serialization;

namespace MadWare.Furs.Responses
{
    [XmlRoot(Namespace = "http://www.fu.gov.si/")]
    public class InvoiceResponseBody : BaseResponseBody
    {
        public InvoiceResponse InvoiceResponse { get; set; }

        public override string GetDataIdValue()
        {
            return this.InvoiceResponse.Id;
        }

        public override bool IsErrorResponse()
        {
            return this.InvoiceResponse.IsErrorResponse();
        }
    }
}