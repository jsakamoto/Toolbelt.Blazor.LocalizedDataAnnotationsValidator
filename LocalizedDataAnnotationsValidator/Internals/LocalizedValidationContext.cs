using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace Toolbelt.Blazor.Forms.Internals
{
    internal class LocalizedValidationContext
    {
        internal IStringLocalizer Localizer { get; }

        internal ValidationContext Context { get; }

        internal LocalizedValidationContext(IStringLocalizer localizer, object instance)
        {
            Localizer = localizer;
            Context = new ValidationContext(instance);
        }

        internal LocalizedValidationContext(IStringLocalizer localizer, ValidationContext context)
        {
            Localizer = localizer;
            Context = context;
        }
    }
}
