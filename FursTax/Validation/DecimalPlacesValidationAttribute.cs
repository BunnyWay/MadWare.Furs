using System;
using System.ComponentModel.DataAnnotations;

namespace MadWare.Furs.Validation
{
    public class DecimalPlacesValidationAttribute : ValidationAttribute
    {
        public int RequiredDecimalPlaces { get; set; }

        public DecimalPlacesValidationAttribute()
        {
            this.RequiredDecimalPlaces = 2;
            this.ErrorMessage = "Decimal value must have exactly 2 decimal places.";
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0} must be rounded to have exactly {1} decimal places.", name, this.RequiredDecimalPlaces);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            decimal v = 0M;
            if (value is decimal)
                v = (decimal)value;
            else if (value is decimal?)
                v = ((decimal?)value).Value;
            else
                throw new NotSupportedException("DecimalPlacesValidationAttribute is only supported on decimal type.");

            int cnt = BitConverter.GetBytes(decimal.GetBits(v)[3])[2];
            return cnt == this.RequiredDecimalPlaces;
        }
    }
}