using MadWare.Furs.Models.BusinessPremise;
using MadWare.Furs.Models.Common;
using MadWare.Furs.Requests;
using MadWare.Furs.Serialization;
using System;
using System.Collections.Generic;
using Xunit;

namespace MadWare.Furs.UnitTest
{
    public class TestXmlSerialization
    {
        [Fact]
        public void TestEchoSerialize()
        {
            IPayloadSerializer s = new XmlPayloadSerializer();

            var e = new EchoRequestBody { EchoRequest = "TEST" };

            string xml = s.SerializeRequest(e);
        }

        [Fact]
        public void TestBusinessPremiseSerialize()
        {
            IPayloadSerializer s = new XmlPayloadSerializer();

            var bp = new BusinessPremiseRequestBody
            {
                BusinessPremiseRequest = new BusinessPremiseRequest
                {
                    BusinessPremise = new BusinessPremise
                    {
                        TaxNumber = "12345678",
                        BusinessPremiseID = "AKTEST",
                        BPIdentifier = new BPIdentifier
                        {
                            RealEstateBP = new RealEstateBP
                            {
                                PropertyID = new PropertyID
                                {
                                    BuildingNumber = 1,
                                    BuildingSectionNumber = 99999,
                                    CadastralNumber = 3
                                },
                                Address = new Address
                                {
                                    HouseNumber = "168",
                                    Street = "DG",
                                    Community = "VRH",
                                    City = "VRH",
                                    PostalCode = "1360"
                                }
                            }
                        },
                        ValidityDate = DateTime.Now.AddYears(1),
                        SoftwareSupplier = new List<SoftwareSupplier>
                        {
                             new SoftwareSupplier
                            {
                                TaxNumber = "87654321"
                            }
                        }
                    }
                }
            };
            bp.BusinessPremiseRequest.Validate();

            string xml = s.SerializeRequest(bp);
        }

        [Fact]
        public void TestInvoiceSerialize()
        {
            IPayloadSerializer s = new XmlPayloadSerializer();

            var inv = new InvoiceRequestBody
            {
                InvoiceRequest = new Models.Invoice.InvoiceRequest
                {
                    Header = new Header
                    {
                        MessageID = Guid.NewGuid().ToString(),
                        DateTime = DateTime.Now
                    },
                    Invoice = new Models.Invoice.Invoice
                    {
                        TaxNumber = "99999862",
                        IssueDateTime = DateTime.Now,
                        NumberingStructure = Models.Invoice.Invoice.NumberingStructureEnum.B,
                        InvoiceIdentifier = new Models.Invoice.InvoiceIdentifier
                        {
                            BusinessPremiseID = "TRGOVINA1",
                            ElectronicDeviceID = "BLAG2",
                            InvoiceNumber = 145
                        },
                        InvoiceAmount = Math.Round((66.7123M), 3),
                        PaymentAmount = Math.Round(1047.76M, 2),
                        TaxesPerSeller = new System.Collections.Generic.List<Models.Invoice.TaxesPerSeller> {
                            new Models.Invoice.TaxesPerSeller
                            {
                                VAT = new System.Collections.Generic.List<Models.Invoice.VAT>
                                {
                                   new Models.Invoice.VAT
                                   {
                                       TaxRate = 22.00M,
                                       TaxableAmount = 23.14M,
                                       TaxAmount = 5.09M
                                   },
                                   new Models.Invoice.VAT
                                   {
                                       TaxRate = 9.50M,
                                       TaxableAmount = 35.14M,
                                       TaxAmount = 3.34M
                                   }
                                }
                            }
                        },
                        OperatorTaxNumber = "12345678"
                    }
                }
            };
            inv.ValidateBody();

            string xml = s.SerializeRequest(inv);
        }

        [Fact]
        public void TestSalesBookInvoiceSerialize()
        {
            IPayloadSerializer s = new XmlPayloadSerializer();

            var inv = new InvoiceRequestBody
            {
                InvoiceRequest = new Models.Invoice.InvoiceRequest
                {
                    Header = new Header
                    {
                        MessageID = Guid.NewGuid().ToString(),
                        DateTime = DateTime.Now
                    },
                    SalesBookInvoice = new Models.Invoice.SalesBookInvoice
                    {
                        TaxNumber = "99999862",
                        IssueDate = DateTime.Now,
                        SalesBookIdentifier = new Models.Invoice.SalesBookIdentifier
                        {
                            InvoiceNumber = "612",
                            SetNumber = "03",
                            SerialNumber = "5001-0001018"
                        },
                        BusinessPremiseID = "TRGOVINA1",
                        InvoiceAmount = 1060.06M,
                        ReturnsAmount = 12.30M,
                        PaymentAmount = 1047.76M,
                        TaxesPerSeller = new System.Collections.Generic.List<Models.Invoice.TaxesPerSeller>
                        {
                            new Models.Invoice.TaxesPerSeller
                            {
                                VAT = new System.Collections.Generic.List<Models.Invoice.VAT>
                                {
                                   new Models.Invoice.VAT
                                   {
                                       TaxRate = 22.00M,
                                       TaxableAmount = 23.14M,
                                       TaxAmount = 5.09M
                                   },
                                   new Models.Invoice.VAT
                                   {
                                       TaxRate = 9.50M,
                                       TaxableAmount = 35.14M,
                                       TaxAmount = 3.34M
                                   }
                                },
                                OtherTaxesAmount = 53.89M,
                                ExemptVATTaxableAmount = 142.87M,
                                ReverseVATTaxableAmount = 67.34M,
                                NontaxableAmount = 43.87M,
                                SpecialTaxRulesAmount = 87.23M
                            },
                            new Models.Invoice.TaxesPerSeller
                            {
                                SellerTaxNumber = "87654321",
                                VAT = new System.Collections.Generic.List<Models.Invoice.VAT>
                                {
                                   new Models.Invoice.VAT
                                   {
                                       TaxRate = 22.00M,
                                       TaxableAmount = 23.14M,
                                       TaxAmount = 5.09M
                                   },
                                   new Models.Invoice.VAT
                                   {
                                       TaxRate = 9.50M,
                                       TaxableAmount = 35.14M,
                                       TaxAmount = 3.34M
                                   }
                                },
                                OtherTaxesAmount = 3.89M,
                                ExemptVATTaxableAmount = 42.87M,
                                ReverseVATTaxableAmount = 6.34M,
                                NontaxableAmount = 4.87M,
                                SpecialTaxRulesAmount = 7.23M
                            }
                        }
                    }
                }
            };

            string xml = s.SerializeRequest(inv);
        }
    }
}