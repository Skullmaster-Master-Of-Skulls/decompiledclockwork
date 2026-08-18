using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D7 RID: 471
	internal abstract class OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey> : QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TOrderKey>
	{
		// Token: 0x06000F79 RID: 3961 RVA: 0x00036C43 File Offset: 0x00034E43
		protected OrderedGroupByQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, Func<TSource, TGroupKey> keySelector, IEqualityComparer<TGroupKey> keyComparer, IComparer<TOrderKey> orderComparer, CancellationToken cancellationToken)
		{
			this.m_source = source;
			this.m_keySelector = keySelector;
			this.m_keyComparer = keyComparer;
			this.m_orderComparer = orderComparer;
			this.m_cancellationToken = cancellationToken;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00036C70 File Offset: 0x00034E70
		internal override bool MoveNext(ref IGrouping<TGroupKey, TElement> currentElement, ref TOrderKey currentKey)
		{
			OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables = this.m_mutables;
			if (mutables == null)
			{
				mutables = (this.m_mutables = new OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables());
				mutables.m_hashLookup = this.BuildHashLookup();
				mutables.m_hashLookupIndex = -1;
			}
			OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables2 = mutables;
			int num = mutables2.m_hashLookupIndex + 1;
			mutables2.m_hashLookupIndex = num;
			if (num < mutables.m_hashLookup.Count)
			{
				OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData value = mutables.m_hashLookup[mutables.m_hashLookupIndex].Value;
				currentElement = value.m_grouping;
				currentKey = value.m_orderKey;
				return true;
			}
			return false;
		}

		// Token: 0x06000F7B RID: 3963
		protected abstract HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData> BuildHashLookup();

		// Token: 0x06000F7C RID: 3964 RVA: 0x00036CF9 File Offset: 0x00034EF9
		protected override void Dispose(bool disposing)
		{
			this.m_source.Dispose();
		}

		// Token: 0x040008D1 RID: 2257
		protected readonly QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> m_source;

		// Token: 0x040008D2 RID: 2258
		private readonly Func<TSource, TGroupKey> m_keySelector;

		// Token: 0x040008D3 RID: 2259
		protected readonly IEqualityComparer<TGroupKey> m_keyComparer;

		// Token: 0x040008D4 RID: 2260
		protected readonly IComparer<TOrderKey> m_orderComparer;

		// Token: 0x040008D5 RID: 2261
		protected readonly CancellationToken m_cancellationToken;

		// Token: 0x040008D6 RID: 2262
		private OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables m_mutables;

		// Token: 0x020003F7 RID: 1015
		private class Mutables
		{
			// Token: 0x040011E7 RID: 4583
			internal HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData> m_hashLookup;

			// Token: 0x040011E8 RID: 4584
			internal int m_hashLookupIndex;
		}

		// Token: 0x020003F8 RID: 1016
		protected class GroupKeyData
		{
			// Token: 0x06001E29 RID: 7721 RVA: 0x0006BF91 File Offset: 0x0006A191
			internal GroupKeyData(TOrderKey orderKey, TGroupKey hashKey, IComparer<TOrderKey> orderComparer)
			{
				this.m_orderKey = orderKey;
				this.m_grouping = new OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement>(hashKey, orderComparer);
			}

			// Token: 0x040011E9 RID: 4585
			internal TOrderKey m_orderKey;

			// Token: 0x040011EA RID: 4586
			internal OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement> m_grouping;
		}
	}
}
