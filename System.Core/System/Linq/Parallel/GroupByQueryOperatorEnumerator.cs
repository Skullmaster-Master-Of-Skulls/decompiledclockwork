using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D4 RID: 468
	internal abstract class GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey> : QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TOrderKey>
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x00036A43 File Offset: 0x00034C43
		protected GroupByQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, IEqualityComparer<TGroupKey> keyComparer, CancellationToken cancellationToken)
		{
			this.m_source = source;
			this.m_keyComparer = keyComparer;
			this.m_cancellationToken = cancellationToken;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00036A60 File Offset: 0x00034C60
		internal override bool MoveNext(ref IGrouping<TGroupKey, TElement> currentElement, ref TOrderKey currentKey)
		{
			GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables = this.m_mutables;
			if (mutables == null)
			{
				mutables = (this.m_mutables = new GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables());
				mutables.m_hashLookup = this.BuildHashLookup();
				mutables.m_hashLookupIndex = -1;
			}
			GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables2 = mutables;
			int num = mutables2.m_hashLookupIndex + 1;
			mutables2.m_hashLookupIndex = num;
			if (num < mutables.m_hashLookup.Count)
			{
				currentElement = new GroupByGrouping<TGroupKey, TElement>(mutables.m_hashLookup[mutables.m_hashLookupIndex]);
				return true;
			}
			return false;
		}

		// Token: 0x06000F73 RID: 3955
		protected abstract HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> BuildHashLookup();

		// Token: 0x06000F74 RID: 3956 RVA: 0x00036AD2 File Offset: 0x00034CD2
		protected override void Dispose(bool disposing)
		{
			this.m_source.Dispose();
		}

		// Token: 0x040008CC RID: 2252
		protected readonly QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> m_source;

		// Token: 0x040008CD RID: 2253
		protected readonly IEqualityComparer<TGroupKey> m_keyComparer;

		// Token: 0x040008CE RID: 2254
		protected readonly CancellationToken m_cancellationToken;

		// Token: 0x040008CF RID: 2255
		private GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables m_mutables;

		// Token: 0x020003F6 RID: 1014
		private class Mutables
		{
			// Token: 0x040011E5 RID: 4581
			internal HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> m_hashLookup;

			// Token: 0x040011E6 RID: 4582
			internal int m_hashLookupIndex;
		}
	}
}
