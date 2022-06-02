using System.Xml.Serialization;

namespace MadWare.Furs.Requests
{
    [XmlRoot(Namespace = "http://www.fu.gov.si/")]
    public class EchoRequestBody : BaseRequestBody
    {
        public string EchoRequest { get; set; }

        public override string GetDataIdValue()
        {
            return null;
        }

        public override string GetSOAPAction()
        {
            return @"/echo";
        }

        public override void ValidateBody()
        {
        }
    }
}