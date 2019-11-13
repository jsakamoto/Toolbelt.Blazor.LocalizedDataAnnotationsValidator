using System.Reflection;

namespace Toolbelt.Blazor.Forms.Internals.Extensions
{
    internal static class ReflectionExtensions
    {
        internal static bool IsPublic(this PropertyInfo p)
        {
            if (!(p.GetMethod != null) || !p.GetMethod.IsPublic)
            {
                if (p.SetMethod != null)
                {
                    return p.SetMethod.IsPublic;
                }
                return false;
            }
            return true;
        }

    }
}
