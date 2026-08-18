using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D6 RID: 470
	internal sealed class GroupByElementSelectorQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey> : GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x00036B88 File Offset: 0x00034D88
		internal GroupByElementSelectorQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, IEqualityComparer<TGroupKey> keyComparer, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken) : base(source, keyComparer, cancellationToken)
		{
			this.m_elementSelector = elementSelector;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00036B9C File Offset: 0x00034D9C
		protected override HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> BuildHashLookup()
		{
			HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> hashLookup = new HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>>(new WrapperEqualityComparer<TGroupKey>(this.m_keyComparer));
			Pair<TSource, TGroupKey> pair = default(Pair<TSource, TGroupKey>);
			TOrderKey torderKey = default(TOrderKey);
			int num = 0;
			while (this.m_source.MoveNext(ref pair, ref torderKey))
			{
				if ((num++ & 63) == 0)
				{
					CancellationState.ThrowIfCanceled(this.m_cancellationToken);
				}
				Wrapper<TGroupKey> key = new Wrapper<TGroupKey>(pair.Second);
				ListChunk<TElement> listChunk = null;
				if (!hashLookup.TryGetValue(key, ref listChunk))
				{
					listChunk = new ListChunk<TElement>(2);
					hashLookup.Add(key, listChunk);
				}
				listChunk.Add(this.m_elementSelector(pair.First));
			}
			return hashLookup;
		}

		// Token: 0x040008D0 RID: 2256
		private readonly Func<TSource, TElement> m_elementSelector;
	}
}
