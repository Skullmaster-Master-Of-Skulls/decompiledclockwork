using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003C7 RID: 967
	internal class CompensatingCollection<TElement> : IOrderedQueryable<TElement>, IQueryable<TElement>, IEnumerable<!0>, IEnumerable, IQueryable, IOrderedQueryable, IOrderedEnumerable<TElement>
	{
		// Token: 0x06003447 RID: 13383 RVA: 0x000CA25F File Offset: 0x000C845F
		public CompensatingCollection(IEnumerable<TElement> source)
		{
			this._source = EntityUtil.CheckArgumentNull<IEnumerable<TElement>>(source, "source");
			this._expression = Expression.Constant(source);
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000CA284 File Offset: 0x000C8484
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x000CA284 File Offset: 0x000C8484
		IEnumerator<TElement> IEnumerable<!0>.GetEnumerator()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x000CA291 File Offset: 0x000C8491
		IOrderedEnumerable<TElement> IOrderedEnumerable<!0>.CreateOrderedEnumerable<K>(Func<TElement, K> keySelector, IComparer<K> comparer, bool descending)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_CreateOrderedEnumerableNotSupported);
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000CA29D File Offset: 0x000C849D
		Type IQueryable.ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x000CA2A9 File Offset: 0x000C84A9
		Expression IQueryable.Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x000CA2B1 File Offset: 0x000C84B1
		IQueryProvider IQueryable.Provider
		{
			get
			{
				throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedQueryableMethod);
			}
		}

		// Token: 0x040016CC RID: 5836
		private readonly IEnumerable<TElement> _source;

		// Token: 0x040016CD RID: 5837
		private readonly Expression _expression;
	}
}
