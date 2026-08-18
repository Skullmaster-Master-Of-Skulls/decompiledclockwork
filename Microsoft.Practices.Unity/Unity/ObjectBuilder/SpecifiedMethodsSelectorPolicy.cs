using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.ObjectBuilder
{
	// Token: 0x0200009A RID: 154
	public class SpecifiedMethodsSelectorPolicy : IMethodSelectorPolicy, IBuilderPolicy
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00008B28 File Offset: 0x00006D28
		public void AddMethodAndParameters(MethodInfo method, IEnumerable<InjectionParameterValue> parameters)
		{
			this.methods.Add(Pair.Make<MethodInfo, IEnumerable<InjectionParameterValue>>(method, parameters));
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00008DCC File Offset: 0x00006FCC
		public IEnumerable<SelectedMethod> SelectMethods(IBuilderContext context, IPolicyList resolverPolicyDestination)
		{
			foreach (Pair<MethodInfo, IEnumerable<InjectionParameterValue>> method in this.methods)
			{
				Type typeToBuild = context.BuildKey.Type;
				ReflectionHelper typeReflector = new ReflectionHelper(method.First.DeclaringType);
				MethodReflectionHelper methodReflector = new MethodReflectionHelper(method.First);
				SelectedMethod selectedMethod;
				if (!methodReflector.MethodHasOpenGenericParameters && !typeReflector.IsOpenGeneric)
				{
					selectedMethod = new SelectedMethod(method.First);
				}
				else
				{
					Type[] closedParameterTypes = methodReflector.GetClosedParameterTypes(typeToBuild.GetTypeInfo().GenericTypeArguments);
					selectedMethod = new SelectedMethod(typeToBuild.GetMethodHierarchical(method.First.Name, closedParameterTypes));
				}
				SpecifiedMemberSelectorHelper.AddParameterResolvers(typeToBuild, resolverPolicyDestination, method.Second, selectedMethod);
				yield return selectedMethod;
			}
			yield break;
		}

		// Token: 0x0400009B RID: 155
		private readonly List<Pair<MethodInfo, IEnumerable<InjectionParameterValue>>> methods = new List<Pair<MethodInfo, IEnumerable<InjectionParameterValue>>>();
	}
}
