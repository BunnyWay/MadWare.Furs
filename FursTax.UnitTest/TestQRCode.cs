using MadWare.Furs.Extensions;
using MadWare.Furs.Models.Common;
using MadWare.Furs.Models.Invoice;
using MadWare.Furs.QRCode;
using System;
using System.Collections.Generic;
using Xunit;

namespace MadWare.Furs.UnitTest
{
    public class TestQRCode
    {
        public static IEnumerable<object[]> TestQRCodeData()
        {
            return new[] {
                new object[] {
                    new QRCodeModel {
                        ZOI = "a7e5f55e1dbb48b799268e1a6d8618a3",
                        TaxNumber = "12345678",
                        InvoiceIssueDateTime = new DateTime(2015, 8, 15, 10, 13, 32)
                    },
                    true,
                    "223175087923687075112234402528973166755123456781508151013321"
                },
                new object[] {
                    new QRCodeModel {
                        ZOI = "3024e56bf1ddd2e7eeb5715c6859a913",
                        TaxNumber = "12345678",
                        InvoiceIssueDateTime = new DateTime(2015, 8, 15, 10, 13, 32)
                    },
                    true,
                    "063994519708649896901260100447252359443123456781508151013320"
                },
                new object[] {
                    new QRCodeModel {
                        ZOI = "3024e56bf1ddd2e7eeb5715c6859a913",
                        TaxNumber = "1234567", //length must be 8, but is is 7
                        InvoiceIssueDateTime = new DateTime(2015, 8, 15, 10, 13, 32)
                    },
                    false,
                    ""
                },
                new object[] {
                    new QRCodeModel {
                        ZOI = "3024e56bf1ddd2e7eeb5715c6859a913",
                        TaxNumber = "12345678"
                    },
                    false,
                    ""
                }
            };
        }

        [Theory]
        [MemberData("TestQRCodeData")]
        public void TestQR(QRCodeModel m, bool valid, string qrCode)
        {
            IQRCodeProvider qrp = new QRCodeProvider();

            try
            {
                string qrCodeTest = qrp.GenerateQRCode(m);
                Assert.Equal(qrCode, qrCodeTest);
            }
            catch
            {
                Assert.Equal(valid, false);
            }
        }

        public static IEnumerable<object[]> TestQRCodeInvoiceData()
        {
            return new[]
            {
                new object[] {
                    new Invoice {
                        ProtectedID =  "a7e5f55e1dbb48b799268e1a6d8618a3",
                        TaxNumber = "12345678",
                        IssueDateTime = new DateTime(2015, 8, 15, 10, 13, 32)
                    },
                    true,
                    "223175087923687075112234402528973166755123456781508151013321"
                },
                new object[] {
                    new Invoice {
                        ProtectedID =  "3024e56bf1ddd2e7eeb5715c6859a913",
                        TaxNumber = "12345678",
                        IssueDateTime = new DateTime(2015, 8, 15, 10, 13, 32)
                    },
                    true,
                    "063994519708649896901260100447252359443123456781508151013320"
                },
                new object[] {
                    new Invoice {
                        ProtectedID =  "3024e56bf1ddd2e7eeb5715c6859a913",
                        TaxNumber = "1234567",
                        IssueDateTime = new DateTime(2015, 8, 15, 10, 13, 32)
                    },
                    false,
                    ""
                },
                new object[] {
                    new Invoice {
                        ProtectedID =  "3024e56bf1ddd2e7eeb5715c6859a913",
                        TaxNumber = "12345678"
                    },
                    false,
                    ""
                }
            };
        }

        [Theory]
        [MemberData("TestQRCodeInvoiceData")]
        public void TestQRModelExtension(Invoice m, bool valid, string qrCode)
        {
            IQRCodeProvider qrp = new QRCodeProvider();

            try
            {
                string qrCodeTest = qrp.GenerateQRCode(m.ToQRCodeModel());
                Assert.Equal(qrCode, qrCodeTest);
            }
            catch
            {
                Assert.Equal(valid, false);
            }
        }

        [Theory]
        [MemberData("TestQRCodeInvoiceData")]
        public void TestGenerateQRExtension(Invoice m, bool valid, string qrCode)
        {
            IQRCodeProvider qrp = new QRCodeProvider();

            try
            {
                string qrCodeTest = m.GenerateQRCode();
                Assert.Equal(qrCode, qrCodeTest);
            }
            catch
            {
                Assert.Equal(valid, false);
            }
        }
    }
}