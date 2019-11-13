using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Toolbelt.Blazor.Forms.Internals.Extensions;

namespace Toolbelt.Blazor.Forms.Internals.ValidationAttributeStores
{
    internal class TypeStoreItem : StoreItem
    {
        private readonly object _syncRoot = new object();

        private readonly Type _type;

        private Dictionary<string, PropertyStoreItem> _propertyStoreItems;

        internal TypeStoreItem(Type type, IEnumerable<Attribute> attributes) : base(attributes)
        {
            _type = type;
        }

        internal PropertyStoreItem GetPropertyStoreItem(string propertyName)
        {
            if (!TryGetPropertyStoreItem(propertyName, out var item)) throw new ArgumentException();
            return item;
        }

        internal bool TryGetPropertyStoreItem(string propertyName, out PropertyStoreItem item)
        {
            if (_propertyStoreItems == null)
            {
                lock (_syncRoot)
                {
                    if (_propertyStoreItems == null)
                    {
                        _propertyStoreItems = CreatePropertyStoreItems();
                    }
                }
            }
            return _propertyStoreItems.TryGetValue(propertyName, out item);
        }

        private Dictionary<string, PropertyStoreItem> CreatePropertyStoreItems()
        {
            var dictionary = new Dictionary<string, PropertyStoreItem>();
            var enumerable = _type.GetRuntimeProperties().Where(prop => prop.IsPublic() && !prop.GetIndexParameters().Any());
            foreach (var item in enumerable)
            {
                var value = new PropertyStoreItem(item.PropertyType, CustomAttributeExtensions.GetCustomAttributes(item, inherit: true));
                dictionary[item.Name] = value;
            }
            return dictionary;
        }
    }
}
