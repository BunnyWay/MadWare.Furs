using System.Xml.Serialization;

namespace MadWare.Furs.Models.Common
{
    public abstract class BaseResponse
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        public Header Header { get; set; }

        public Error Error { get; set; }

        public bool IsErrorResponse()
        {
            return this.Error != null;
        }
    }
}