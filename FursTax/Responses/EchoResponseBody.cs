using System.Xml.Serialization;

namespace MadWare.Furs.Responses
{
    [XmlRoot(Namespace = "http://www.fu.gov.si/")]
    public class EchoResponseBody : BaseResponseBody
    {
        public string EchoResponse { get; set; }

        public override string GetDataIdValue()
        {
            return null;
        }

        public override bool IsErrorResponse()
        {
            return false;
        }
    }
}