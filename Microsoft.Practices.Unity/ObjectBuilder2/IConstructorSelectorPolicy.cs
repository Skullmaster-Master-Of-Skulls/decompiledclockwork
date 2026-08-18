using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200004D RID: 77
	public interface IConstructorSelectorPolicy : IBuilderPolicy
	{
		// Token: 0x06000144 RID: 324
		SelectedConstructor SelectConstructor(IBuilderContext context, IPolicyList resolverPolicyDestination);
	}
}
