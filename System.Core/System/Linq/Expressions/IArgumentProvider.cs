using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200023F RID: 575
	[__DynamicallyInvokable]
	public interface IArgumentProvider
	{
		// Token: 0x0600152C RID: 5420
		[__DynamicallyInvokable]
		Expression GetArgument(int index);

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600152D RID: 5421
		[__DynamicallyInvokable]
		int ArgumentCount { [__DynamicallyInvokable] get; }
	}
}
