using System.ComponentModel.DataAnnotations;

namespace Toolbelt.Blazor.Forms.Internals.Validators
{
    internal class ValidationError
    {
        private readonly object _value;

        private readonly ValidationAttribute _validationAttribute;

        internal ValidationResult ValidationResult { get; }

        internal ValidationError(ValidationAttribute validationAttribute, object value, ValidationResult validationResult)
        {
            _validationAttribute = validationAttribute;
            ValidationResult = validationResult;
            _value = value;
        }
    }
}
