using System;

namespace System.Collections.Immutable
{
	// Token: 0x0200000D RID: 13
	internal interface IImmutableArray
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000059 RID: 89
		Array Array { get; }

		// Token: 0x0600005A RID: 90
		void ThrowInvalidOperationIfNotInitialized();
	}
}
