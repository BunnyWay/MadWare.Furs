using System.Security.Cryptography.X509Certificates;

namespace MadWare.Furs.UnitTest
{
    public abstract class BaseTestWithCert
    {
        public string TestUrl { get; set; }
        public X509Certificate2 Cert { get; set; }

        public BaseTestWithCert()
        {
            this.TestUrl = "https://blagajne-test.fu.gov.si:9002/v1/cash_registers";
            this.Cert = new X509Certificate2("10442529-1.p12", "SAMR6ADL8IE6");
        }
    }
}