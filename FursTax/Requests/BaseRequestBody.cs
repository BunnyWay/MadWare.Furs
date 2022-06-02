using MadWare.Furs.Serialization;
using System.Xml.Serialization;

namespace MadWare.Furs.Requests
{
    [XmlInclude(typeof(Requests.EchoRequestBody))]
    [XmlInclude(typeof(Requests.BusinessPremiseRequestBody))]
    [XmlInclude(typeof(Requests.InvoiceRequestBody))]
    [XmlRoot(Namespace = "http://www.fu.gov.si/")]
    public abstract class BaseRequestBody : BaseBody
    {
        /// <summary>
        /// Get SOAP action of the request
        /// </summary>
        public abstract string GetSOAPAction();

        /// <summary>
        /// Validates the data of the request
        /// </summary>
        public abstract void ValidateBody();
    }
}