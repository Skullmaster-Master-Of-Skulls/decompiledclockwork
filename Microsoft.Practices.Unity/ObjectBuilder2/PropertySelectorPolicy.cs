using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000069 RID: 105
	public class PropertySelectorPolicy<TResolutionAttribute> : PropertySelectorBase<TResolutionAttribute> where TResolutionAttribute : Attribute
	{
		// Token: 0x060001BD RID: 445 RVA: 0x000068EC File Offset: 0x00004AEC
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		protected override IDependencyResolverPolicy CreateResolver(PropertyInfo property)
		{
			Guard.ArgumentNotNull(property, "property");
			return new FixedTypeResolverPolicy(property.PropertyType);
		}
	}
}
