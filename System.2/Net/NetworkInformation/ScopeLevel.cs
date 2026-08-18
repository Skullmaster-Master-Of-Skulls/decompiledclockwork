using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002A3 RID: 675
	[__DynamicallyInvokable]
	public enum ScopeLevel
	{
		// Token: 0x040018D2 RID: 6354
		[__DynamicallyInvokable]
		None,
		// Token: 0x040018D3 RID: 6355
		[__DynamicallyInvokable]
		Interface,
		// Token: 0x040018D4 RID: 6356
		[__DynamicallyInvokable]
		Link,
		// Token: 0x040018D5 RID: 6357
		[__DynamicallyInvokable]
		Subnet,
		// Token: 0x040018D6 RID: 6358
		[__DynamicallyInvokable]
		Admin,
		// Token: 0x040018D7 RID: 6359
		[__DynamicallyInvokable]
		Site,
		// Token: 0x040018D8 RID: 6360
		[__DynamicallyInvokable]
		Organization = 8,
		// Token: 0x040018D9 RID: 6361
		[__DynamicallyInvokable]
		Global = 14
	}
}
