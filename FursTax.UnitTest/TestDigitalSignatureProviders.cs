using MadWare.Furs.Encryption;
using MadWare.Furs.Requests;
using MadWare.Furs.Serialization;
using System;
using System.Collections.Generic;
using Xunit;

namespace MadWare.Furs.UnitTest
{
    public class TestDigitalSignatureProviders : BaseTestWithCert
    {
        public static IEnumerable<object[]> TestDataCalculateSigInvoice()
        {
            return new[] {
                new object[] {
                    new InvoiceRequestBody { InvoiceRequest = new Models.Invoice.InvoiceRequest {
                        Invoice = new Models.Invoice.Invoice {
                            TaxNumber = "12345678",
                            IssueDateTime = new DateTime(2016, 3, 20, 20, 0, 0),
                            InvoiceIdentifier = new Models.Invoice.InvoiceIdentifier { BusinessPremiseID = "PP1", ElectronicDeviceID = "EN1", InvoiceNumber = 123 },
                            InvoiceAmount = 12.34M
                        } } }
                }
            };
        }

        [Theory]
        [MemberData("TestDataCalculateSigInvoice")]
        public void TestDigitalSignatureXmlInvoice(BaseRequestBody b)
        {
            IPayloadSerializer s = new XmlPayloadSerializer();
            string payload = s.SerializeRequest(b);

            var cert = this.Cert;
            IDigitalSignatureProvider sig = new CertXmlDigitalSignatureProvider(cert);

            string signedPayload = sig.SignRequest(payload, b);
        }
    }
}