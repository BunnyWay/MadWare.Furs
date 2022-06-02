using MadWare.Furs.Models.Common;
using MadWare.Furs.Models.Invoice;
using MadWare.Furs.QRCode;

namespace MadWare.Furs.Extensions
{
    public static class ModelHelperExtensions
    {
        /// <summary>
        /// Converts Invoice model to QRCodeModel class that is suitable for IQRCodeProvider
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static QRCodeModel ToQRCodeModel(this Invoice i)
        {
            return new QRCodeModel
            {
                InvoiceIssueDateTime = i.IssueDateTime,
                TaxNumber = i.TaxNumber,
                ZOI = i.ProtectedID
            };
        }

        /// <summary>
        /// Generates QRCode for the invoice
        /// </summary>
        /// <param name="i"></param>
        /// <param name="qrp">Optional custom implementation of IQRCodeProvider</param>
        /// <returns></returns>
        public static string GenerateQRCode(this Invoice i, IQRCodeProvider qrp = null)
        {
            var qrm = i.ToQRCodeModel();

            if (qrp == null)
                qrp = new QRCodeProvider();

            return qrp.GenerateQRCode(qrm);
        }
    }
}