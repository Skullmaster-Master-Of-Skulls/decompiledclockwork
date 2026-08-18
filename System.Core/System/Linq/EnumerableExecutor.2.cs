using System;
using System.Linq.Expressions;

namespace System.Linq
{
	// Token: 0x02000166 RID: 358
	[__DynamicallyInvokable]
	public class EnumerableExecutor<T> : EnumerableExecutor
	{
		// Token: 0x06000C56 RID: 3158 RVA: 0x0002D77D File Offset: 0x0002B97D
		[__DynamicallyInvokable]
		public EnumerableExecutor(Expression expression)
		{
			this.expression = expression;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002D78C File Offset: 0x0002B98C
		internal override object ExecuteBoxed()
		{
			return this.Execute();
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002D79C File Offset: 0x0002B99C
		internal T Execute()
		{
			if (this.func == null)
			{
				EnumerableRewriter enumerableRewriter = new EnumerableRewriter();
				Expression body = enumerableRewriter.Visit(this.expression);
				Expression<Func<T>> expression = Expression.Lambda<Func<T>>(body, null);
				this.func = expression.Compile();
			}
			return this.func();
		}

		// Token: 0x040007A4 RID: 1956
		private Expression expression;

		// Token: 0x040007A5 RID: 1957
		private Func<T> func;
	}
}
