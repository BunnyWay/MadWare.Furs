using MadWare.Furs.Serialization;
using System.Xml.Serialization;

namespace MadWare.Furs.Responses
{
    [XmlInclude(typeof(Responses.EchoResponseBody))]
    [XmlInclude(typeof(Responses.InvoiceResponseBody))]
    [XmlInclude(typeof(Responses.BusinessPremiseResponseBody))]
    [XmlRoot(Namespace = "http://www.fu.gov.si/")]
    public abstract class BaseResponseBody : BaseBody
    {
        public abstract bool IsErrorResponse();
    }
}