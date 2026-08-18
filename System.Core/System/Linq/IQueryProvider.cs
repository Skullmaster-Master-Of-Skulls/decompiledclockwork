using System;
using System.Linq.Expressions;

namespace System.Linq
{
	// Token: 0x0200014B RID: 331
	[__DynamicallyInvokable]
	public interface IQueryProvider
	{
		// Token: 0x06000AA4 RID: 2724
		[__DynamicallyInvokable]
		IQueryable CreateQuery(Expression expression);

		// Token: 0x06000AA5 RID: 2725
		[__DynamicallyInvokable]
		IQueryable<TElement> CreateQuery<TElement>(Expression expression);

		// Token: 0x06000AA6 RID: 2726
		[__DynamicallyInvokable]
		object Execute(Expression expression);

		// Token: 0x06000AA7 RID: 2727
		[__DynamicallyInvokable]
		TResult Execute<TResult>(Expression expression);
	}
}
