using MadWare.Furs.Requests;
using MadWare.Furs.Responses;
using MadWare.Furs.Serialization;
using System.Collections.Generic;
using Xunit;

namespace MadWare.Furs.UnitTest
{
    public class TestXmlDeserialization
    {
        public static IEnumerable<object[]> TestEchoResponses()
        {
            return new[] {
                new object[] { @"<?xml version='1.0' encoding='UTF-8'?>
                                <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ds='http://www.w3.org/2000/09/xmldsig#' xmlns:fu='http://www.fu.gov.si/' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                   <soapenv:Body>
                                      <fu:EchoResponse>test</fu:EchoResponse>
                                   </soapenv:Body>
                                </soapenv:Envelope>", "test" },
                new object[] { @"<?xml version='1.0' encoding='UTF-8'?>
                                <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ds='http://www.w3.org/2000/09/xmldsig#' xmlns:fu='http://www.fu.gov.si/' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                   <soapenv:Body>
                                      <fu:EchoResponse>test2</fu:EchoResponse>
                                   </soapenv:Body>
                                </soapenv:Envelope>", "test2" },
            };
        }

        [Theory]
        [MemberData("TestEchoResponses")]
        public void TestEchoDeserialize(string r, string value)
        {
            IPayloadSerializer s = new XmlPayloadSerializer();

            var echo = s.DeserializeResponse(r, typeof(EchoRequestBody)) as EchoResponseBody;

            Assert.Equal(value, echo.EchoResponse);
        }

        public static IEnumerable<object[]> TestInvoiceResponses()
        {
            return new[] {
                new object[] { @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                                xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:fu='http://www.fu.gov.si/'
                                xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                 <soapenv:Body>
                                     <fu:InvoiceResponse Id='data'>
                                         <fu:Header>
                                             <fu:DateTime>2015-08-07T13:06:52.631Z</fu:DateTime>
                                             <fu:MessageID>4e64a93a-40fa-4c02-afb1-488534b85e4c</fu:MessageID>
                                         </fu:Header>
                                     <fu:UniqueInvoiceID>b5f1c310-3dea-4331-82f8-d2dc72d9d018</fu:UniqueInvoiceID>
                                      </fu:InvoiceResponse>
                                 </soapenv:Body>
                                </soapenv:Envelope>", true},
                new object[] { @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                                xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:fu='http://www.fu.gov.si/'
                                xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                 <soapenv:Body>
                                 <fu:InvoiceResponse Id='data'>
                                     <fu:Header>
                                         <fu:DateTime>2015-08-07T13:06:52.631Z</fu:DateTime>
                                         <fu:MessageID>4e64a93a-40fa-4c02-afb1-488534b85e4c</fu:MessageID>
                                     </fu:Header>
                                 <fu:Error>
                                    <fu:ErrorCode>s001</fu:ErrorCode>
                                    <fu:ErrorMessage> Sporočilo ni v skladu z XML shemo.</fu:ErrorMessage>
                                 </fu:Error>
                                 </fu:InvoiceResponse>
                                 </soapenv:Body>
                                </soapenv:Envelope>", false },
            };
        }

        [Theory]
        [MemberData("TestInvoiceResponses")]
        public void TestInvoiceDeserialize(string r, bool valid)
        {
            IPayloadSerializer s = new XmlPayloadSerializer();

            var inv = s.DeserializeResponse(r, typeof(InvoiceRequestBody)) as InvoiceResponseBody;

            Assert.Equal(2015, inv.InvoiceResponse.Header.DateTime.Value.Year);
            Assert.Equal(8, inv.InvoiceResponse.Header.DateTime.Value.Month);
            Assert.Equal(7, inv.InvoiceResponse.Header.DateTime.Value.Day);

            Assert.Equal("4e64a93a-40fa-4c02-afb1-488534b85e4c", inv.InvoiceResponse.Header.MessageID);

            Assert.Equal(valid, !inv.InvoiceResponse.IsErrorResponse());
            if (valid)
            {
                Assert.Equal("b5f1c310-3dea-4331-82f8-d2dc72d9d018", inv.InvoiceResponse.UniqueInvoiceID);
                Assert.NotNull(inv.InvoiceResponse.UniqueInvoiceID);
            }
            else
            {
                Assert.NotNull(inv.InvoiceResponse.Error);
                Assert.Equal("s001", inv.InvoiceResponse.Error.ErrorCode);
            }
        }

        public static IEnumerable<object[]> TestBusinessPremiseResponses()
        {
            return new[] {
                new object[] { @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                                xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:fu='http://www.fu.gov.si/'
                                xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                 <soapenv:Body>
                                     <fu:BusinessPremiseResponse Id='data'>
                                         <fu:Header>
                                             <fu:DateTime>2015-08-07T13:06:52.631Z</fu:DateTime>
                                             <fu:MessageID>4e64a93a-40fa-4c02-afb1-488534b85e4c</fu:MessageID>
                                         </fu:Header>
                                      </fu:BusinessPremiseResponse>
                                 </soapenv:Body>
                                </soapenv:Envelope>", true},
                new object[] { @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                                xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:fu='http://www.fu.gov.si/'
                                xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                 <soapenv:Body>
                                 <fu:BusinessPremiseResponse Id='data'>
                                     <fu:Header>
                                         <fu:DateTime>2015-08-07T13:06:52.631Z</fu:DateTime>
                                         <fu:MessageID>4e64a93a-40fa-4c02-afb1-488534b85e4c</fu:MessageID>
                                     </fu:Header>
                                 <fu:Error>
                                    <fu:ErrorCode>s001</fu:ErrorCode>
                                    <fu:ErrorMessage> Sporočilo ni v skladu z XML shemo.</fu:ErrorMessage>
                                 </fu:Error>
                                 </fu:BusinessPremiseResponse>
                                 </soapenv:Body>
                                </soapenv:Envelope>", false },
            };
        }

        [Theory]
        [MemberData("TestBusinessPremiseResponses")]
        public void TestBusinessPremiseDeserialize(string r, bool valid)
        {
            IPayloadSerializer s = new XmlPayloadSerializer();

            var bp = s.DeserializeResponse(r, typeof(BusinessPremiseRequestBody)) as BusinessPremiseResponseBody;

            Assert.Equal(2015, bp.BusinessPremiseResponse.Header.DateTime.Value.Year);
            Assert.Equal(8, bp.BusinessPremiseResponse.Header.DateTime.Value.Month);
            Assert.Equal(7, bp.BusinessPremiseResponse.Header.DateTime.Value.Day);

            Assert.Equal("4e64a93a-40fa-4c02-afb1-488534b85e4c", bp.BusinessPremiseResponse.Header.MessageID);

            Assert.Equal(valid, !bp.BusinessPremiseResponse.IsErrorResponse());
            if (valid)
            {
            }
            else
            {
                Assert.NotNull(bp.BusinessPremiseResponse.Error);
                Assert.Equal("s001", bp.BusinessPremiseResponse.Error.ErrorCode);
            }
        }
    }
}