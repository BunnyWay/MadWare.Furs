﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MadWare.Furs.Serialization
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope<T> where T : BaseRequestBody
    {
        public T Body { get; set; }
    }

    public abstract class BaseRequestBody
    {
        /// <summary>
        /// Get the Id reference value on root element for signing
        /// </summary>
        /// <returns>Value</returns>
        public abstract string GetDataIdValue();
    }

}


