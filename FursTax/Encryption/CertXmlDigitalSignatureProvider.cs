using MadWare.Furs.Requests;
using MadWare.Furs.Responses;
using System.Deployment.Internal.CodeSigning;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace MadWare.Furs.Encryption
{
    public class CertXmlDigitalSignatureProvider : IDigitalSignatureProvider
    {
        private X509Certificate2 cert;

        public CertXmlDigitalSignatureProvider(X509Certificate2 cert)
        {
            this.cert = cert;
        }

        public string SignRequest(string requestPayload, BaseRequestBody b)
        {
            //Get id value of the main payload node
            string dataId = b.GetDataIdValue();

            if (string.IsNullOrEmpty(dataId))
                return requestPayload;

            XmlDocument msg = new XmlDocument();
            msg.LoadXml(requestPayload);

            //get the reference of the main data node - first node with attr Id=dataId
            XmlNode dataNode = msg.SelectSingleNode(string.Format("//*[@Id='{0}']", dataId));

            if (dataNode == null)
                return requestPayload;

            CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), SignedXml.XmlDsigRSASHA256Url);

            SignedXml xmlSig = new SignedXml(msg);

            byte[] pubKey = this.cert.GetPublicKey();
            string pubKeyBase64 = Convert.ToBase64String(pubKey);

            RSACryptoServiceProvider rsaCSP = (RSACryptoServiceProvider)this.cert.PrivateKey;
            CspParameters cspParams = new CspParameters();
            cspParams.KeyContainerName = rsaCSP.CspKeyContainerInfo.KeyContainerName;
            cspParams.KeyNumber = rsaCSP.CspKeyContainerInfo.KeyNumber == KeyNumber.Exchange ? 1 : 2;
            RSACryptoServiceProvider rsaAesCSP = new RSACryptoServiceProvider(cspParams);

            xmlSig.SigningKey = rsaAesCSP;

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
            keyInfoData.AddIssuerSerial(this.cert.Issuer, this.cert.SerialNumber);

            X509Extension extension = this.cert.Extensions[1];
            AsnEncodedData asndata = new AsnEncodedData(extension.Oid, extension.RawData);
            keyInfoData.AddSubjectName(this.cert.SubjectName.Name);

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "#" + dataId;
            reference.DigestMethod = @"http://www.w3.org/2001/04/xmlenc#sha256";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            xmlSig.AddReference(reference);

            keyInfo.AddClause(keyInfoData);
            xmlSig.KeyInfo = keyInfo;
            xmlSig.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

            // Compute the signature.
            xmlSig.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = xmlSig.GetXml();

            dataNode.AppendChild(xmlDigitalSignature);

            return msg.InnerXml;
        }

        public bool VerifyResponseSignature(string responsePayload, BaseResponseBody b)
        {
            return true;
        }
    }
}