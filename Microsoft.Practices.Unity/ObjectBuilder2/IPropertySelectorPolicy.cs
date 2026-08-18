using System;
using System.Collections.Generic;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000067 RID: 103
	public interface IPropertySelectorPolicy : IBuilderPolicy
	{
		// Token: 0x060001B7 RID: 439
		IEnumerable<SelectedProperty> SelectProperties(IBuilderContext context, IPolicyList resolverPolicyDestination);
	}
}
