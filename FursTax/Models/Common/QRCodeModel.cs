using System;
using System.ComponentModel.DataAnnotations;

namespace MadWare.Furs.Models.Common
{
    public class QRCodeModel : BaseModel
    {
        [Required(), StringLength(32, MinimumLength = 32)]
        public string ZOI { get; set; }

        /// <summary>
        /// Davčna številka zavezanca (8 mest) / tax number of the person liable (length 8)
        /// </summary>
        [Required(), StringLength(8, MinimumLength = 8)]
        public string TaxNumber { get; set; }

        [Required]
        public DateTime? InvoiceIssueDateTime { get; set; }
    }
}