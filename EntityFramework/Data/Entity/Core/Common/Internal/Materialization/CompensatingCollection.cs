using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002DA RID: 730
	internal class CompensatingCollection<TElement> : IOrderedQueryable<TElement>, IQueryable<TElement>, IOrderedQueryable, IQueryable, IOrderedEnumerable<TElement>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06001996 RID: 6550 RVA: 0x0007FAA7 File Offset: 0x0007DCA7
		public CompensatingCollection(IEnumerable<TElement> source)
		{
			this._source = source;
			this._expression = Expression.Constant(source);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0007FAC2 File Offset: 0x0007DCC2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0007FACF File Offset: 0x0007DCCF
		IEnumerator<TElement> IEnumerable<!0>.GetEnumerator()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0007FADC File Offset: 0x0007DCDC
		IOrderedEnumerable<TElement> IOrderedEnumerable<!0>.CreateOrderedEnumerable<K>(Func<TElement, K> keySelector, IComparer<K> comparer, bool descending)
		{
			throw new NotSupportedException(Strings.ELinq_CreateOrderedEnumerableNotSupported);
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x0007FAE8 File Offset: 0x0007DCE8
		Type IQueryable.ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x0007FAF4 File Offset: 0x0007DCF4
		Expression IQueryable.Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x0007FAFC File Offset: 0x0007DCFC
		IQueryProvider IQueryable.Provider
		{
			get
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedQueryableMethod);
			}
		}

		// Token: 0x040008CF RID: 2255
		private readonly IEnumerable<TElement> _source;

		// Token: 0x040008D0 RID: 2256
		private readonly Expression _expression;
	}
}
