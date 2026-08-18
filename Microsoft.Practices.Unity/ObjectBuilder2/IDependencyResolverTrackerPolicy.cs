using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200006B RID: 107
	public interface IDependencyResolverTrackerPolicy : IBuilderPolicy
	{
		// Token: 0x060001C2 RID: 450
		void AddResolverKey(object key);

		// Token: 0x060001C3 RID: 451
		void RemoveResolvers(IPolicyList policies);
	}
}
