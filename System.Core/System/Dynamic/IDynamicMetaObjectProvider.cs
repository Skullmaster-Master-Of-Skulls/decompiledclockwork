using System;
using System.Linq.Expressions;

namespace System.Dynamic
{
	// Token: 0x020000CA RID: 202
	[__DynamicallyInvokable]
	public interface IDynamicMetaObjectProvider
	{
		// Token: 0x06000606 RID: 1542
		[__DynamicallyInvokable]
		DynamicMetaObject GetMetaObject(Expression parameter);
	}
}
