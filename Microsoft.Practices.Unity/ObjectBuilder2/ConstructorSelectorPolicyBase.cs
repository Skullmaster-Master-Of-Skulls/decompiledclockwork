using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200004E RID: 78
	public abstract class ConstructorSelectorPolicyBase<TInjectionConstructorMarkerAttribute> : IConstructorSelectorPolicy, IBuilderPolicy where TInjectionConstructorMarkerAttribute : Attribute
	{
		// Token: 0x06000145 RID: 325 RVA: 0x000045B8 File Offset: 0x000027B8
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public SelectedConstructor SelectConstructor(IBuilderContext context, IPolicyList resolverPolicyDestination)
		{
			Guard.ArgumentNotNull(context, "context");
			Type type = context.BuildKey.Type;
			ConstructorInfo constructorInfo = ConstructorSelectorPolicyBase<TInjectionConstructorMarkerAttribute>.FindInjectionConstructor(type) ?? ConstructorSelectorPolicyBase<TInjectionConstructorMarkerAttribute>.FindLongestConstructor(type);
			if (constructorInfo != null)
			{
				return this.CreateSelectedConstructor(constructorInfo);
			}
			return null;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000045FC File Offset: 0x000027FC
		private SelectedConstructor CreateSelectedConstructor(ConstructorInfo ctor)
		{
			SelectedConstructor selectedConstructor = new SelectedConstructor(ctor);
			foreach (ParameterInfo parameter in ctor.GetParameters())
			{
				selectedConstructor.AddParameterResolver(this.CreateResolver(parameter));
			}
			return selectedConstructor;
		}

		// Token: 0x06000147 RID: 327
		protected abstract IDependencyResolverPolicy CreateResolver(ParameterInfo parameter);

		// Token: 0x06000148 RID: 328 RVA: 0x0000464C File Offset: 0x0000284C
		private static ConstructorInfo FindInjectionConstructor(Type typeToConstruct)
		{
			ReflectionHelper reflectionHelper = new ReflectionHelper(typeToConstruct);
			ConstructorInfo[] array = (from ctor in reflectionHelper.InstanceConstructors
			where ctor.IsDefined(typeof(TInjectionConstructorMarkerAttribute), true)
			select ctor).ToArray<ConstructorInfo>();
			switch (array.Length)
			{
			case 0:
				return null;
			case 1:
				return array[0];
			default:
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.MultipleInjectionConstructors, new object[]
				{
					typeToConstruct.GetTypeInfo().Name
				}));
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000046D4 File Offset: 0x000028D4
		private static ConstructorInfo FindLongestConstructor(Type typeToConstruct)
		{
			ReflectionHelper reflectionHelper = new ReflectionHelper(typeToConstruct);
			ConstructorInfo[] array = reflectionHelper.InstanceConstructors.ToArray<ConstructorInfo>();
			Array.Sort<ConstructorInfo>(array, new ConstructorSelectorPolicyBase<TInjectionConstructorMarkerAttribute>.ConstructorLengthComparer());
			switch (array.Length)
			{
			case 0:
				return null;
			case 1:
				return array[0];
			default:
			{
				int num = array[0].GetParameters().Length;
				if (array[1].GetParameters().Length == num)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.AmbiguousInjectionConstructor, new object[]
					{
						typeToConstruct.GetTypeInfo().Name,
						num
					}));
				}
				return array[0];
			}
			}
		}

		// Token: 0x0200004F RID: 79
		private class ConstructorLengthComparer : IComparer<ConstructorInfo>
		{
			// Token: 0x0600014C RID: 332 RVA: 0x00004774 File Offset: 0x00002974
			[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
			public int Compare(ConstructorInfo x, ConstructorInfo y)
			{
				Guard.ArgumentNotNull(x, "x");
				Guard.ArgumentNotNull(y, "y");
				return y.GetParameters().Length - x.GetParameters().Length;
			}
		}
	}
}
