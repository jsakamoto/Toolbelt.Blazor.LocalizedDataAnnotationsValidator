using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Toolbelt.Blazor.Forms.Internals.ValidationAttributeStores
{
    internal class StoreItem
    {
        internal IEnumerable<ValidationAttribute> ValidationAttributes { get; }

        internal DisplayAttribute DisplayAttribute { get; }

        internal StoreItem(IEnumerable<Attribute> attributes)
        {
            ValidationAttributes = attributes.OfType<ValidationAttribute>();
            DisplayAttribute = attributes.OfType<DisplayAttribute>().SingleOrDefault();
        }
    }
}
