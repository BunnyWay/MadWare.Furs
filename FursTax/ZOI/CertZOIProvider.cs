using MadWare.Furs.Models.Invoice;
using MadWare.Furs.Requests;
using System.Globalization;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MadWare.Furs.ZOI
{
    public class CertZOIProvider : IZOIProvider
    {
        private X509Certificate2 cert;

        public CertZOIProvider(X509Certificate2 cert)
        {
            this.cert = cert;
        }

        private string GetMD5Hash(byte[] input)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(input);
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++) { sBuilder.Append(data[i].ToString("x2")); }
            return sBuilder.ToString();
        }

        public void CalculateZOI(BaseRequestBody b)
        {
            var inv = b as InvoiceRequestBody;
            Invoice i = inv.InvoiceRequest.Invoice;

            string data = string.Concat(i.TaxNumber,
                                         i.IssueDateTime.Value.ToString("dd.MM.yyyy HH:mm:ss"),
                                         i.InvoiceIdentifier.InvoiceNumber,
                                         i.InvoiceIdentifier.BusinessPremiseID,
                                         i.InvoiceIdentifier.ElectronicDeviceID,
                                         i.InvoiceAmount.Value.ToString("N2", CultureInfo.InvariantCulture));

            byte[] bytes = Encoding.ASCII.GetBytes(data);

            RSACryptoServiceProvider rsaCSP = (RSACryptoServiceProvider)this.cert.PrivateKey;
            CspParameters cspParameters = new CspParameters();
            cspParameters.KeyContainerName = rsaCSP.CspKeyContainerInfo.KeyContainerName;
            cspParameters.KeyNumber = rsaCSP.CspKeyContainerInfo.KeyNumber == KeyNumber.Exchange ? 1 : 2;

            RSACryptoServiceProvider rsaAesCSP = new RSACryptoServiceProvider(cspParameters);
            byte[] signature = rsaAesCSP.SignData(bytes, CryptoConfig.MapNameToOID("SHA256"));

            string zoi = GetMD5Hash(signature);

            i.ProtectedID = zoi;
        }

        public bool MustCalculateZOI(BaseRequestBody b)
        {
            if (b == null)
                return false;

            if (!(b is InvoiceRequestBody))
                return false;

            var invReqBody = b as InvoiceRequestBody;
            if (invReqBody.InvoiceRequest == null || invReqBody.InvoiceRequest.Invoice == null)
                return false;

            return true;
        }
    }
}