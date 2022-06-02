using MadWare.Furs.Models.BusinessPremise;
using System.Xml.Serialization;

namespace MadWare.Furs.Responses
{
    [XmlRoot(Namespace = "http://www.fu.gov.si/")]
    public class BusinessPremiseResponseBody : BaseResponseBody
    {
        public BusinessPremiseResponse BusinessPremiseResponse { get; set; }

        public override string GetDataIdValue()
        {
            return this.BusinessPremiseResponse.Id;
        }

        public override bool IsErrorResponse()
        {
            return this.BusinessPremiseResponse.IsErrorResponse();
        }
    }
}