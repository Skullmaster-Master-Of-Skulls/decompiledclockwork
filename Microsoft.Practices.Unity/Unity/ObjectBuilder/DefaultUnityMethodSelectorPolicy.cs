using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.ObjectBuilder
{
	// Token: 0x02000092 RID: 146
	public class DefaultUnityMethodSelectorPolicy : MethodSelectorPolicyBase<InjectionMethodAttribute>
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x00008838 File Offset: 0x00006A38
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		protected override IDependencyResolverPolicy CreateResolver(ParameterInfo parameter)
		{
			Guard.ArgumentNotNull(parameter, "parameter");
			List<DependencyResolutionAttribute> list = parameter.GetCustomAttributes(false).OfType<DependencyResolutionAttribute>().ToList<DependencyResolutionAttribute>();
			if (list.Count > 0)
			{
				return list[0].CreateResolver(parameter.ParameterType);
			}
			return new NamedTypeDependencyResolverPolicy(parameter.ParameterType, null);
		}
	}
}
