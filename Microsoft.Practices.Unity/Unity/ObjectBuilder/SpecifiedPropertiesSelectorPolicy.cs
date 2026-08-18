using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.ObjectBuilder
{
	// Token: 0x0200009B RID: 155
	public class SpecifiedPropertiesSelectorPolicy : IPropertySelectorPolicy, IBuilderPolicy
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00008E0A File Offset: 0x0000700A
		public void AddPropertyAndValue(PropertyInfo property, InjectionParameterValue value)
		{
			this.propertiesAndValues.Add(Pair.Make<PropertyInfo, InjectionParameterValue>(property, value));
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00009064 File Offset: 0x00007264
		public IEnumerable<SelectedProperty> SelectProperties(IBuilderContext context, IPolicyList resolverPolicyDestination)
		{
			Type typeToBuild = context.BuildKey.Type;
			ReflectionHelper currentTypeReflector = new ReflectionHelper(context.BuildKey.Type);
			foreach (Pair<PropertyInfo, InjectionParameterValue> pair in this.propertiesAndValues)
			{
				PropertyInfo currentProperty = pair.First;
				if (new ReflectionHelper(pair.First.DeclaringType).IsOpenGeneric)
				{
					currentProperty = currentTypeReflector.Type.GetTypeInfo().GetDeclaredProperty(currentProperty.Name);
				}
				yield return new SelectedProperty(currentProperty, pair.Second.GetResolverPolicy(typeToBuild));
			}
			yield break;
		}

		// Token: 0x0400009C RID: 156
		private readonly List<Pair<PropertyInfo, InjectionParameterValue>> propertiesAndValues = new List<Pair<PropertyInfo, InjectionParameterValue>>();
	}
}
