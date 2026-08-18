using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
	// Token: 0x02000165 RID: 357
	[__DynamicallyInvokable]
	public abstract class EnumerableExecutor
	{
		// Token: 0x06000C53 RID: 3155
		internal abstract object ExecuteBoxed();

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002D730 File Offset: 0x0002B930
		internal static EnumerableExecutor Create(Expression expression)
		{
			Type type = typeof(EnumerableExecutor<>).MakeGenericType(new Type[]
			{
				expression.Type
			});
			return (EnumerableExecutor)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[]
			{
				expression
			}, null);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002D775 File Offset: 0x0002B975
		[__DynamicallyInvokable]
		protected EnumerableExecutor()
		{
		}
	}
}
