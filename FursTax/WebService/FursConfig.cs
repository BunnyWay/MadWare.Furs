using System.Security.Cryptography.X509Certificates;

namespace MadWare.Furs.WebService
{
    public class FursConfig
    {
        public string Url { get; set; }

        public X509Certificate2 Certificate { get; set; }

        /// <summary>
        /// Timeout in seconds when calling service. Default value is 2.
        /// </summary>
        public int HttpTimeout = 2;
    }
}