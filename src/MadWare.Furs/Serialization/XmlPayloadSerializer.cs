﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadWare.Furs.Requests;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using MadWare.Furs.Responses;
using MadWare.Furs.Serialization;

namespace MadWare.Furs.Serialization
{
    public class XmlPayloadSerializer : IPayloadSerializer
    {
        public BaseResponseBody DeserializeResponse(string r, Type typeOfRequestBody)
        {

            XmlSerializer xSer = null; ;

            if (typeOfRequestBody == typeof(EchoRequestBody))
            {
                xSer = new XmlSerializer(typeof(Envelope<EchoResponseBody>));
            }
            else if (typeOfRequestBody == typeof(InvoiceRequestBody))
            {
                xSer = new XmlSerializer(typeof(Envelope<InvoiceResponseBody>));
            }
            else if(typeOfRequestBody == typeof(BusinessPremiseRequestBody))
            {
                xSer = new XmlSerializer(typeof(Envelope<BusinessPremiseResponseBody>));
            }
            else
            {
                throw new NotSupportedException("This typeOfRequestBody not supported!");
            }

            using (StringReader sr = new StringReader(r))
            {
                var e = xSer.Deserialize(sr);
                var pInfoBody = e.GetType().GetProperty("Body");
                return (BaseResponseBody)pInfoBody.GetValue(e);
            }
        }

        public string SerializeRequest(BaseRequestBody b)
        {
            var e = new Envelope<BaseRequestBody> { Body = b };

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            ns.Add("fu", "http://www.fu.gov.si/");
            ns.Add("xd", "http://www.w3.org/2000/09/xmldsig#");

            XmlDocument doc = new XmlDocument();
            XmlSerializer xSer = new XmlSerializer(e.GetType());
            using (StringWriter sw = new StringWriter())
            {
                using (XmlWriter xmlW = XmlWriter.Create(sw))
                {
                    xSer.Serialize(xmlW, e, ns);
                    doc.LoadXml(sw.ToString());
                }
            }

            string dataId = b.GetDataIdValue();
            if (dataId != null)
            {
                XmlNode dataNode = doc.SelectSingleNode(string.Format("//*[@Id='{0}']", dataId));
                XmlNode body = dataNode.ParentNode;
                body.Attributes.RemoveAll();
            }

            return doc.InnerXml;
        }
    }
}
