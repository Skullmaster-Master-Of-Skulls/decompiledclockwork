using System;
using System.Collections.Generic;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000063 RID: 99
	public interface IMethodSelectorPolicy : IBuilderPolicy
	{
		// Token: 0x060001AD RID: 429
		IEnumerable<SelectedMethod> SelectMethods(IBuilderContext context, IPolicyList resolverPolicyDestination);
	}
}
