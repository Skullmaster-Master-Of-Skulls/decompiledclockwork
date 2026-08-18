using System;

namespace System.ComponentModel
{
	// Token: 0x0200055B RID: 1371
	[__DynamicallyInvokable]
	public interface IChangeTracking
	{
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06003381 RID: 13185
		[__DynamicallyInvokable]
		bool IsChanged { [__DynamicallyInvokable] get; }

		// Token: 0x06003382 RID: 13186
		[__DynamicallyInvokable]
		void AcceptChanges();
	}
}
