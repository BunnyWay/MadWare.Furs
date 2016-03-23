﻿using MadWare.Furs.Models.BusinessPremise;
using MadWare.Furs.Serialization;
using System.Xml.Serialization;
using System;

namespace MadWare.Furs.Requests
{
    [XmlRoot(Namespace = "http://www.fu.gov.si/")]
    public class BusinessPremiseRequestBody : BaseRequestBody
    {
        public BusinessPremiseRequest BusinessPremiseRequest { get; set; }

        public override string GetDataIdValue()
        {
            return this.BusinessPremiseRequest.Id;
        }

        public override string GetSOAPAction()
        {
            return @"/invoices/register";
        }

        public override void ValidateBody()
        {
            this.BusinessPremiseRequest.Validate();
        }
    }
}
