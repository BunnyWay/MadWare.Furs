using MadWare.Furs.Requests;
using MadWare.Furs.ZOI;
using System;
using System.Collections.Generic;
using Xunit;

namespace MadWare.Furs.UnitTest
{
    public class TestZOICalculation : BaseTestWithCert
    {
        public static IEnumerable<object[]> TestDataMustCalculateZOI()
        {
            return new[] {
                new object[] {
                     new InvoiceRequestBody { InvoiceRequest = new Models.Invoice.InvoiceRequest { Invoice = new Models.Invoice.Invoice() } },
                    true
                },
                new object[] {
                    new InvoiceRequestBody { InvoiceRequest = new Models.Invoice.InvoiceRequest { } },
                    false
                },
                new object[] {
                    new InvoiceRequestBody { InvoiceRequest = new Models.Invoice.InvoiceRequest { SalesBookInvoice = new Models.Invoice.SalesBookInvoice() } },
                    false
                },
                new object[] {
                    new BusinessPremiseRequestBody { BusinessPremiseRequest = new Models.BusinessPremise.BusinessPremiseRequest { } },
                    false
                }
            };
        }

        [Theory]
        [MemberData("TestDataMustCalculateZOI")]
        public void TestMustCalculateZOI(BaseRequestBody b, bool doCalc)
        {
            IZOIProvider zoiProvider = new CertZOIProvider(null);

            Assert.Equal(doCalc, zoiProvider.MustCalculateZOI(b));
        }

        public static IEnumerable<object[]> TestDataCalculateZOI()
        {
            return new[] {
                new object[] {
                    new InvoiceRequestBody { InvoiceRequest = new Models.Invoice.InvoiceRequest {
                        Invoice = new Models.Invoice.Invoice {
                            TaxNumber = "12345678",
                            IssueDateTime = new DateTime(2016, 3, 20, 20, 0, 0),
                            InvoiceIdentifier = new Models.Invoice.InvoiceIdentifier { BusinessPremiseID = "PP1", ElectronicDeviceID = "EN1", InvoiceNumber = 123 },
                            InvoiceAmount = 12.34M
                        } } },
                    "937ceef29ef74129f4b384185736919c" //would be different for different certificates
                },
                new object[] {
                    new InvoiceRequestBody { InvoiceRequest = new Models.Invoice.InvoiceRequest {
                        Invoice = new Models.Invoice.Invoice {
                            TaxNumber = "12345678",
                            IssueDateTime = new DateTime(2016, 3, 20, 20, 0, 0),
                            InvoiceIdentifier = new Models.Invoice.InvoiceIdentifier { BusinessPremiseID = "PP1", ElectronicDeviceID = "EN1", InvoiceNumber = 123 },
                            InvoiceAmount = 12.34M,
                            OperatorTaxNumber = "87654321"
                        } } },
                    "937ceef29ef74129f4b384185736919c"//would be different for different certificates
                }
            };
        }

        [Theory]
        [MemberData("TestDataCalculateZOI")]
        public void TestCalculateZOI(InvoiceRequestBody b, string expectedZoi)
        {
            var cert = this.Cert;
            IZOIProvider zoiProvider = new CertZOIProvider(cert);

            zoiProvider.CalculateZOI(b);

            string zoi = b.InvoiceRequest.Invoice.ProtectedID;

            Assert.Equal(expectedZoi, zoi);
        }
    }
}