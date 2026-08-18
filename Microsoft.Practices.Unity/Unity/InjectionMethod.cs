using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000084 RID: 132
	public class InjectionMethod : InjectionMember
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00007E2C File Offset: 0x0000602C
		public InjectionMethod(string methodName, params object[] methodParameters)
		{
			this.methodName = methodName;
			this.methodParameters = InjectionParameterValue.ToParameters(methodParameters).ToList<InjectionParameterValue>();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00007E4C File Offset: 0x0000604C
		public override void AddPolicies(Type serviceType, Type implementationType, string name, IPolicyList policies)
		{
			MethodInfo method = this.FindMethod(implementationType);
			this.ValidateMethodCanBeInjected(method, implementationType);
			SpecifiedMethodsSelectorPolicy selectorPolicy = InjectionMethod.GetSelectorPolicy(policies, implementationType, name);
			selectorPolicy.AddMethodAndParameters(method, this.methodParameters);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007E80 File Offset: 0x00006080
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		protected virtual bool MethodNameMatches(MemberInfo targetMethod, string nameToMatch)
		{
			Guard.ArgumentNotNull(targetMethod, "targetMethod");
			return targetMethod.Name == nameToMatch;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00007E9C File Offset: 0x0000609C
		private MethodInfo FindMethod(Type typeToCreate)
		{
			ParameterMatcher parameterMatcher = new ParameterMatcher(this.methodParameters);
			foreach (MethodInfo methodInfo in typeToCreate.GetMethodsHierarchical())
			{
				if (this.MethodNameMatches(methodInfo, this.methodName) && parameterMatcher.Matches(methodInfo.GetParameters()))
				{
					return methodInfo;
				}
			}
			return null;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007F14 File Offset: 0x00006114
		private void ValidateMethodCanBeInjected(MethodInfo method, Type typeToCreate)
		{
			this.GuardMethodNotNull(method, typeToCreate);
			this.GuardMethodNotStatic(method, typeToCreate);
			this.GuardMethodNotGeneric(method, typeToCreate);
			this.GuardMethodHasNoOutParams(method, typeToCreate);
			this.GuardMethodHasNoRefParams(method, typeToCreate);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00007F3E File Offset: 0x0000613E
		private void GuardMethodNotNull(MethodInfo info, Type typeToCreate)
		{
			if (info == null)
			{
				this.ThrowIllegalInjectionMethod(Resources.NoSuchMethod, typeToCreate);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00007F4F File Offset: 0x0000614F
		private void GuardMethodNotStatic(MethodInfo info, Type typeToCreate)
		{
			if (info.IsStatic)
			{
				this.ThrowIllegalInjectionMethod(Resources.CannotInjectStaticMethod, typeToCreate);
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007F65 File Offset: 0x00006165
		private void GuardMethodNotGeneric(MethodInfo info, Type typeToCreate)
		{
			if (info.IsGenericMethodDefinition)
			{
				this.ThrowIllegalInjectionMethod(Resources.CannotInjectGenericMethod, typeToCreate);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00007F83 File Offset: 0x00006183
		private void GuardMethodHasNoOutParams(MethodInfo info, Type typeToCreate)
		{
			if (info.GetParameters().Any((ParameterInfo param) => param.IsOut))
			{
				this.ThrowIllegalInjectionMethod(Resources.CannotInjectMethodWithOutParams, typeToCreate);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00007FC8 File Offset: 0x000061C8
		private void GuardMethodHasNoRefParams(MethodInfo info, Type typeToCreate)
		{
			if (info.GetParameters().Any((ParameterInfo param) => param.ParameterType.IsByRef))
			{
				this.ThrowIllegalInjectionMethod(Resources.CannotInjectMethodWithRefParams, typeToCreate);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008008 File Offset: 0x00006208
		private void ThrowIllegalInjectionMethod(string message, Type typeToCreate)
		{
			IFormatProvider currentCulture = CultureInfo.CurrentCulture;
			object[] array = new object[3];
			array[0] = typeToCreate.GetTypeInfo().Name;
			array[1] = this.methodName;
			array[2] = this.methodParameters.JoinStrings(", ", (InjectionParameterValue mp) => mp.ParameterTypeName);
			throw new InvalidOperationException(string.Format(currentCulture, message, array));
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008074 File Offset: 0x00006274
		private static SpecifiedMethodsSelectorPolicy GetSelectorPolicy(IPolicyList policies, Type typeToCreate, string name)
		{
			NamedTypeBuildKey buildKey = new NamedTypeBuildKey(typeToCreate, name);
			IMethodSelectorPolicy methodSelectorPolicy = policies.GetNoDefault(buildKey, false);
			if (methodSelectorPolicy == null || !(methodSelectorPolicy is SpecifiedMethodsSelectorPolicy))
			{
				methodSelectorPolicy = new SpecifiedMethodsSelectorPolicy();
				policies.Set(methodSelectorPolicy, buildKey);
			}
			return (SpecifiedMethodsSelectorPolicy)methodSelectorPolicy;
		}

		// Token: 0x04000082 RID: 130
		private readonly string methodName;

		// Token: 0x04000083 RID: 131
		private readonly List<InjectionParameterValue> methodParameters;
	}
}
