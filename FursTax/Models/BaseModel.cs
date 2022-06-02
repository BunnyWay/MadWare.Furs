using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MadWare.Furs.Models
{
    public abstract class BaseModel
    {
        public virtual void Validate()
        {
            var vr = new List<ValidationResult>();
            var vc = new ValidationContext(this);
            if (!Validator.TryValidateObject(this, vc, vr, true))
            {
                throw new Exception(string.Format("{0} not valid! \n{1}",
                                                   this.GetType().Name,
                                                   string.Join(",", vr.Select(r => r.ErrorMessage))));
            }
        }
    }
}