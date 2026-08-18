using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.ObjectBuilder
{
	// Token: 0x02000093 RID: 147
	public class DefaultUnityPropertySelectorPolicy : PropertySelectorBase<DependencyResolutionAttribute>
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00008894 File Offset: 0x00006A94
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		protected override IDependencyResolverPolicy CreateResolver(PropertyInfo property)
		{
			Guard.ArgumentNotNull(property, "property");
			List<DependencyResolutionAttribute> list = property.GetCustomAttributes(typeof(DependencyResolutionAttribute), false).OfType<DependencyResolutionAttribute>().ToList<DependencyResolutionAttribute>();
			return list[0].CreateResolver(property.PropertyType);
		}
	}
}
