using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000065 RID: 101
	public class MethodSelectorPolicy<TMarkerAttribute> : MethodSelectorPolicyBase<TMarkerAttribute> where TMarkerAttribute : Attribute
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x0000662B File Offset: 0x0000482B
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		protected override IDependencyResolverPolicy CreateResolver(ParameterInfo parameter)
		{
			Guard.ArgumentNotNull(parameter, "parameter");
			return new FixedTypeResolverPolicy(parameter.ParameterType);
		}
	}
}
