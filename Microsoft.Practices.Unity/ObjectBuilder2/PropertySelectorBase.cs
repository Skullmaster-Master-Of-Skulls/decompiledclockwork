using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000068 RID: 104
	public abstract class PropertySelectorBase<TResolutionAttribute> : IPropertySelectorPolicy, IBuilderPolicy where TResolutionAttribute : Attribute
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x000068A4 File Offset: 0x00004AA4
		public virtual IEnumerable<SelectedProperty> SelectProperties(IBuilderContext context, IPolicyList resolverPolicyDestination)
		{
			Type t = context.BuildKey.Type;
			foreach (PropertyInfo prop in from p in t.GetPropertiesHierarchical()
			where p.CanWrite
			select p)
			{
				MethodInfo propertyMethod = prop.SetMethod ?? prop.GetMethod;
				if (!propertyMethod.IsStatic && prop.GetIndexParameters().Length == 0 && prop.IsDefined(typeof(TResolutionAttribute), false))
				{
					yield return this.CreateSelectedProperty(prop);
				}
			}
			yield break;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000068C8 File Offset: 0x00004AC8
		private SelectedProperty CreateSelectedProperty(PropertyInfo property)
		{
			IDependencyResolverPolicy resolver = this.CreateResolver(property);
			return new SelectedProperty(property, resolver);
		}

		// Token: 0x060001BA RID: 442
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Property", Justification = "Identifier name 'property' makes sense. Avoid changing API names.")]
		protected abstract IDependencyResolverPolicy CreateResolver(PropertyInfo property);
	}
}
