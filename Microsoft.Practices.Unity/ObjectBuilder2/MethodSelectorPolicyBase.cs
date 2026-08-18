using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000064 RID: 100
	public abstract class MethodSelectorPolicyBase<TMarkerAttribute> : IMethodSelectorPolicy, IBuilderPolicy where TMarkerAttribute : Attribute
	{
		// Token: 0x060001AE RID: 430 RVA: 0x000065C4 File Offset: 0x000047C4
		public virtual IEnumerable<SelectedMethod> SelectMethods(IBuilderContext context, IPolicyList resolverPolicyDestination)
		{
			Type t = context.BuildKey.Type;
			IEnumerable<MethodInfo> candidateMethods = from m in t.GetMethodsHierarchical()
			where !m.IsStatic && m.IsPublic
			select m;
			foreach (MethodInfo method in candidateMethods)
			{
				if (method.IsDefined(typeof(TMarkerAttribute), false))
				{
					yield return this.CreateSelectedMethod(method);
				}
			}
			yield break;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000065E8 File Offset: 0x000047E8
		private SelectedMethod CreateSelectedMethod(MethodInfo method)
		{
			SelectedMethod selectedMethod = new SelectedMethod(method);
			foreach (ParameterInfo parameter in method.GetParameters())
			{
				selectedMethod.AddParameterResolver(this.CreateResolver(parameter));
			}
			return selectedMethod;
		}

		// Token: 0x060001B0 RID: 432
		protected abstract IDependencyResolverPolicy CreateResolver(ParameterInfo parameter);
	}
}
