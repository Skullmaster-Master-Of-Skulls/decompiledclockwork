using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000144 RID: 324
	[__DynamicallyInvokable]
	public interface IRuntimeVariables
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000A6F RID: 2671
		[__DynamicallyInvokable]
		int Count { [__DynamicallyInvokable] get; }

		// Token: 0x17000213 RID: 531
		[__DynamicallyInvokable]
		object this[int index]
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}
	}
}
