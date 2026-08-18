using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000083 RID: 131
	public class InjectionConstructor : InjectionMember
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00007CF8 File Offset: 0x00005EF8
		public InjectionConstructor(params object[] parameterValues)
		{
			this.parameterValues = InjectionParameterValue.ToParameters(parameterValues).ToList<InjectionParameterValue>();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00007D14 File Offset: 0x00005F14
		public override void AddPolicies(Type serviceType, Type implementationType, string name, IPolicyList policies)
		{
			ConstructorInfo ctor = this.FindConstructor(implementationType);
			policies.Set(new SpecifiedConstructorSelectorPolicy(ctor, this.parameterValues.ToArray()), new NamedTypeBuildKey(implementationType, name));
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007D50 File Offset: 0x00005F50
		private ConstructorInfo FindConstructor(Type typeToCreate)
		{
			ParameterMatcher parameterMatcher = new ParameterMatcher(this.parameterValues);
			ReflectionHelper reflectionHelper = new ReflectionHelper(typeToCreate);
			foreach (ConstructorInfo constructorInfo in reflectionHelper.InstanceConstructors)
			{
				if (parameterMatcher.Matches(constructorInfo.GetParameters()))
				{
					return constructorInfo;
				}
			}
			string text = string.Join(", ", (from p in this.parameterValues
			select p.ParameterTypeName).ToArray<string>());
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.NoSuchConstructor, new object[]
			{
				typeToCreate.FullName,
				text
			}));
		}

		// Token: 0x04000080 RID: 128
		private readonly List<InjectionParameterValue> parameterValues;
	}
}
