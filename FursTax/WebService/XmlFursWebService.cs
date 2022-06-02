using MadWare.Furs.Encryption;
using MadWare.Furs.Http;
using MadWare.Furs.Serialization;
using MadWare.Furs.ZOI;

namespace MadWare.Furs.WebService
{
    public class XmlFursWebService : BaseFursWebService
    {
        public FursConfig FursConfig { get; protected set; }

        public XmlFursWebService(FursConfig fursConfig)
            : base(fursConfig.Url,
                  new XmlPayloadSerializer(),
                  new CertZOIProvider(fursConfig.Certificate),
                  new CertXmlDigitalSignatureProvider(fursConfig.Certificate),
                  new SoapHttpService(fursConfig.Certificate, fursConfig.HttpTimeout))
        {
            this.FursConfig = FursConfig;
        }
    }
}