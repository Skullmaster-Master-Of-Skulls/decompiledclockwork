using System;

namespace System.ComponentModel
{
	// Token: 0x02000575 RID: 1397
	[__DynamicallyInvokable]
	public interface IRevertibleChangeTracking : IChangeTracking
	{
		// Token: 0x060033E5 RID: 13285
		[__DynamicallyInvokable]
		void RejectChanges();
	}
}
