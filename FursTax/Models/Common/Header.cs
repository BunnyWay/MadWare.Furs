using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MadWare.Furs.Models.Common
{
    public class Header : BaseModel
    {
        /// <summary>
        /// Enkratni identifikator sporočila. / Unique identifier of the message
        /// Vsako sporočilo mora imeti enkratno identifikacijsko oznako.
        /// Enako velja tudi pri pošiljanju sporočila, ki se zaradi napake pošilja ponovno. / Every message shall have the unique identification mark.The same is valid also for sending messages, which are resent due to errors.
        /// </summary>
        [Required(), StringLength(36, MinimumLength = 36)]
        public string MessageID { get; set; }

        /// <summary>
        /// Datum in čas pošiljanja sporočila. / Date and time of sending the message
        /// </summary>
        [XmlIgnore]
        [Required]
        public DateTime? DateTime { get; set; }

        [XmlElement("DateTime")]
        public string DateTimeFormatted
        {
            get
            {
                return this.DateTime.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            set { this.DateTime = Convert.ToDateTime(value); }
        }

        public Header()
        {
            this.MessageID = Guid.NewGuid().ToString();
            this.DateTime = System.DateTime.Now;
        }
    }
}