using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.ObjectBuilder
{
	// Token: 0x02000098 RID: 152
	public class SpecifiedConstructorSelectorPolicy : IConstructorSelectorPolicy, IBuilderPolicy
	{
		// Token: 0x060002BA RID: 698 RVA: 0x00008A0A File Offset: 0x00006C0A
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "ctor", Justification = "Parameter name is meaningful enough in context (ctor is in common use)")]
		public SpecifiedConstructorSelectorPolicy(ConstructorInfo ctor, InjectionParameterValue[] parameterValues)
		{
			this.ctor = ctor;
			this.ctorReflector = new MethodReflectionHelper(ctor);
			this.parameterValues = parameterValues;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00008A2C File Offset: 0x00006C2C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public SelectedConstructor SelectConstructor(IBuilderContext context, IPolicyList resolverPolicyDestination)
		{
			Guard.ArgumentNotNull(context, "context");
			Type type = context.BuildKey.Type;
			ReflectionHelper reflectionHelper = new ReflectionHelper(this.ctor.DeclaringType);
			SelectedConstructor result;
			if (!this.ctorReflector.MethodHasOpenGenericParameters && !reflectionHelper.IsOpenGeneric)
			{
				result = new SelectedConstructor(this.ctor);
			}
			else
			{
				Type[] closedParameterTypes = this.ctorReflector.GetClosedParameterTypes(type.GenericTypeArguments);
				result = new SelectedConstructor(type.GetConstructor(closedParameterTypes));
			}
			SpecifiedMemberSelectorHelper.AddParameterResolvers(type, resolverPolicyDestination, this.parameterValues, result);
			return result;
		}

		// Token: 0x04000098 RID: 152
		private readonly ConstructorInfo ctor;

		// Token: 0x04000099 RID: 153
		private readonly MethodReflectionHelper ctorReflector;

		// Token: 0x0400009A RID: 154
		private readonly InjectionParameterValue[] parameterValues;
	}
}
