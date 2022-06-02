using MadWare.Furs.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MadWare.Furs.QRCode
{
    public class QRCodeProvider : IQRCodeProvider
    {
        //https://stackoverflow.com/questions/16965915
        private string HexToDecimal(string hex)
        {
            List<int> dec = new List<int> { 0 };   // decimal result

            foreach (char c in hex)
            {
                int carry = Convert.ToInt32(c.ToString(), 16);

                // initially holds decimal value of current hex digit;
                // subsequently holds carry-over for multiplication
                for (int i = 0; i < dec.Count; ++i)
                {
                    int val = (dec[i] * 16) + carry;
                    dec[i] = val % 10;
                    carry = val / 10;
                }

                while (carry > 0)
                {
                    dec.Add(carry % 10);
                    carry /= 10;
                }
            }

            var chars = dec.Select(d => (char)('0' + d));
            var cArr = chars.Reverse().ToArray();
            return new string(cArr);
        }

        private int Modulo10(string value)
        {
            int len = value.Length;
            int sum = 0;
            for (int i = len - 1; i >= 0; i--)
            {
                int tmp = (value[i] - '0');
                sum += tmp;
            }

            int modulo10 = sum % 10;
            return modulo10;
        }

        public string GenerateQRCode(QRCodeModel model)
        {
            model.Validate();

            string decimalZoi = this.HexToDecimal(model.ZOI).PadLeft(39, '0');

            StringBuilder sb = new StringBuilder(70);
            sb.Append(decimalZoi);
            sb.Append(model.TaxNumber);
            sb.Append(model.InvoiceIssueDateTime.Value.ToString("yyMMddHHmmss"));

            int m10 = this.Modulo10(sb.ToString());
            sb.Append(m10.ToString());

            return sb.ToString();
        }
    }
}