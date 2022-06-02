using MadWare.Furs.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MadWare.Furs.Models.BusinessPremise
{
    public class BusinessPremiseRequest : BaseModel
    {
        [XmlAttribute()]
        public string Id = "data";

        /// <summary>
        /// Glava sporočila / Message header
        /// </summary>
        [Required]
        public Header Header { get; set; }

        /// <summary>
        /// Poslovni prostor / Business premise
        /// </summary>
        [Required]
        public BusinessPremise BusinessPremise { get; set; }

        public BusinessPremiseRequest()
        {
            this.Header = new Header();
        }

        public override void Validate()
        {
            base.Validate();

            this.Header.Validate();
            this.BusinessPremise.Validate();
        }
    }
}