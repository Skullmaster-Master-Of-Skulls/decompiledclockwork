using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000050 RID: 80
	public class ConstructorSelectorPolicy<TInjectionConstructorMarkerAttribute> : ConstructorSelectorPolicyBase<TInjectionConstructorMarkerAttribute> where TInjectionConstructorMarkerAttribute : Attribute
	{
		// Token: 0x0600014E RID: 334 RVA: 0x000047A5 File Offset: 0x000029A5
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		protected override IDependencyResolverPolicy CreateResolver(ParameterInfo parameter)
		{
			Guard.ArgumentNotNull(parameter, "parameter");
			return new FixedTypeResolverPolicy(parameter.ParameterType);
		}
	}
}
