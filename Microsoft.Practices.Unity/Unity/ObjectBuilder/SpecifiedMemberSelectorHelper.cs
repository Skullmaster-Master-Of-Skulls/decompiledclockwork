using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.ObjectBuilder
{
	// Token: 0x02000099 RID: 153
	public static class SpecifiedMemberSelectorHelper
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00008AB4 File Offset: 0x00006CB4
		public static void AddParameterResolvers(Type typeToBuild, IPolicyList policies, IEnumerable<InjectionParameterValue> parameterValues, SelectedMemberWithParameters result)
		{
			Guard.ArgumentNotNull(policies, "policies");
			Guard.ArgumentNotNull(parameterValues, "parameterValues");
			Guard.ArgumentNotNull(result, "result");
			foreach (InjectionParameterValue injectionParameterValue in parameterValues)
			{
				IDependencyResolverPolicy resolverPolicy = injectionParameterValue.GetResolverPolicy(typeToBuild);
				result.AddParameterResolver(resolverPolicy);
			}
		}
	}
}
