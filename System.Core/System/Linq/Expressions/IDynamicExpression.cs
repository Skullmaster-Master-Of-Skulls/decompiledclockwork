using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000241 RID: 577
	[__DynamicallyInvokable]
	public interface IDynamicExpression : IArgumentProvider
	{
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600152F RID: 5423
		[__DynamicallyInvokable]
		Type DelegateType { [__DynamicallyInvokable] get; }

		// Token: 0x06001530 RID: 5424
		[__DynamicallyInvokable]
		Expression Rewrite(Expression[] args);

		// Token: 0x06001531 RID: 5425
		[__DynamicallyInvokable]
		object CreateCallSite();
	}
}
