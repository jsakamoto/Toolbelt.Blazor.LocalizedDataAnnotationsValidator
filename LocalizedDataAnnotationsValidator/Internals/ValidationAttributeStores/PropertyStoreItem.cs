using System;
using System.Collections.Generic;

namespace Toolbelt.Blazor.Forms.Internals.ValidationAttributeStores
{
    internal class PropertyStoreItem : StoreItem
    {
        internal Type PropertyType { get; }

        internal PropertyStoreItem(Type propertyType, IEnumerable<Attribute> attributes) : base(attributes)
        {
            PropertyType = propertyType;
        }
    }
}
