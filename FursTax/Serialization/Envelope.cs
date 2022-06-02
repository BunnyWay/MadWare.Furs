using System.Xml.Serialization;

namespace MadWare.Furs.Serialization
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope<T> where T : BaseBody
    {
        public T Body { get; set; }
    }

    public abstract class BaseBody
    {
        /// <summary>
        /// Get the Id reference value on root element for signing
        /// </summary>
        /// <returns>Value</returns>
        public abstract string GetDataIdValue();
    }
}