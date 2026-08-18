using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
	// Token: 0x02000163 RID: 355
	[__DynamicallyInvokable]
	public abstract class EnumerableQuery
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000C3F RID: 3135
		internal abstract Expression Expression { get; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000C40 RID: 3136
		internal abstract IEnumerable Enumerable { get; }

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002D49C File Offset: 0x0002B69C
		internal static IQueryable Create(Type elementType, IEnumerable sequence)
		{
			Type type = typeof(EnumerableQuery<>).MakeGenericType(new Type[]
			{
				elementType
			});
			return (IQueryable)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[]
			{
				sequence
			}, null);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002D4DC File Offset: 0x0002B6DC
		internal static IQueryable Create(Type elementType, Expression expression)
		{
			Type type = typeof(EnumerableQuery<>).MakeGenericType(new Type[]
			{
				elementType
			});
			return (IQueryable)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[]
			{
				expression
			}, null);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002D51C File Offset: 0x0002B71C
		[__DynamicallyInvokable]
		protected EnumerableQuery()
		{
		}
	}
}
