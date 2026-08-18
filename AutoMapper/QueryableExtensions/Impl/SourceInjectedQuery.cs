using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x0200006B RID: 107
	public class SourceInjectedQuery<TSource, TDestination> : IOrderedQueryable<TDestination>, IQueryable<TDestination>, IEnumerable<TDestination>, IEnumerable, IQueryable, IOrderedQueryable
	{
		// Token: 0x060003AC RID: 940 RVA: 0x000094BC File Offset: 0x000076BC
		public SourceInjectedQuery(IQueryable<TSource> dataSource, IQueryable<TDestination> destQuery, IMapper mapper, SourceInjectedQueryInspector inspector = null)
		{
			this.Expression = destQuery.Expression;
			this.ElementType = typeof(TDestination);
			this.Provider = new SourceInjectedQueryProvider<TSource, TDestination>(mapper, dataSource, destQuery)
			{
				Inspector = (inspector ?? new SourceInjectedQueryInspector())
			};
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000950A File Offset: 0x0000770A
		internal SourceInjectedQuery(IQueryProvider provider, Expression expression)
		{
			this.Provider = provider;
			this.Expression = expression;
			this.ElementType = typeof(TDestination);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00009530 File Offset: 0x00007730
		public IEnumerator<TDestination> GetEnumerator()
		{
			return this.Provider.Execute<IEnumerable<TDestination>>(this.Expression).GetEnumerator();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00009548 File Offset: 0x00007748
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00009550 File Offset: 0x00007750
		public Type ElementType { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00009558 File Offset: 0x00007758
		public Expression Expression { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00009560 File Offset: 0x00007760
		public IQueryProvider Provider { get; }
	}
}
