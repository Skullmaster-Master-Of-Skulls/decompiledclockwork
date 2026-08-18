using System;
using System.Collections;
using System.Linq.Expressions;

namespace System.Linq
{
	// Token: 0x02000149 RID: 329
	[__DynamicallyInvokable]
	public interface IQueryable : IEnumerable
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000AA1 RID: 2721
		[__DynamicallyInvokable]
		Expression Expression { [__DynamicallyInvokable] get; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000AA2 RID: 2722
		[__DynamicallyInvokable]
		Type ElementType { [__DynamicallyInvokable] get; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000AA3 RID: 2723
		[__DynamicallyInvokable]
		IQueryProvider Provider { [__DynamicallyInvokable] get; }
	}
}
