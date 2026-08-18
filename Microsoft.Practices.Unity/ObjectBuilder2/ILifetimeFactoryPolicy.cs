using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000070 RID: 112
	public interface ILifetimeFactoryPolicy : IBuilderPolicy
	{
		// Token: 0x060001D0 RID: 464
		ILifetimePolicy CreateLifetimePolicy();

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001D1 RID: 465
		Type LifetimeType { get; }
	}
}
