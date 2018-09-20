using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.Model.Inputs
{
    public abstract class BaseInput
    {
        private List<ValidationResult> _validationResults;

        public BaseInput()
        {

            _validationResults = new List<ValidationResult>();
        }

        public bool IsValid()
        {
            var validationContext = new ValidationContext(this, null, null);
            return Validator.TryValidateObject(this, validationContext, _validationResults, true);
        }

        public string GetErrorMessage()
        {
            return _validationResults.FirstOrDefault().ErrorMessage;
        }
    }
}
