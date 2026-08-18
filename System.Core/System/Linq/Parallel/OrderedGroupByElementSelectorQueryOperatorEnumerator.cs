using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D9 RID: 473
	internal sealed class OrderedGroupByElementSelectorQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey> : OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>
	{
		// Token: 0x06000F7F RID: 3967 RVA: 0x00036E1D File Offset: 0x0003501D
		internal OrderedGroupByElementSelectorQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, Func<TSource, TGroupKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TGroupKey> keyComparer, IComparer<TOrderKey> orderComparer, CancellationToken cancellationToken) : base(source, keySelector, keyComparer, orderComparer, cancellationToken)
		{
			this.m_elementSelector = elementSelector;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00036E34 File Offset: 0x00035034
		protected override HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData> BuildHashLookup()
		{
			HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData> hashLookup = new HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData>(new WrapperEqualityComparer<TGroupKey>(this.m_keyComparer));
			Pair<TSource, TGroupKey> pair = default(Pair<TSource, TGroupKey>);
			TOrderKey torderKey = default(TOrderKey);
			int num = 0;
			while (this.m_source.MoveNext(ref pair, ref torderKey))
			{
				if ((num++ & 63) == 0)
				{
					CancellationState.ThrowIfCanceled(this.m_cancellationToken);
				}
				Wrapper<TGroupKey> wrapper = new Wrapper<TGroupKey>(pair.Second);
				OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData groupKeyData = null;
				if (hashLookup.TryGetValue(wrapper, ref groupKeyData))
				{
					if (this.m_orderComparer.Compare(torderKey, groupKeyData.m_orderKey) < 0)
					{
						groupKeyData.m_orderKey = torderKey;
					}
				}
				else
				{
					groupKeyData = new OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData(torderKey, wrapper.Value, this.m_orderComparer);
					hashLookup.Add(wrapper, groupKeyData);
				}
				groupKeyData.m_grouping.Add(this.m_elementSelector(pair.First), torderKey);
			}
			for (int i = 0; i < hashLookup.Count; i++)
			{
				hashLookup[i].Value.m_grouping.DoneAdding();
			}
			return hashLookup;
		}

		// Token: 0x040008D7 RID: 2263
		private readonly Func<TSource, TElement> m_elementSelector;
	}
}
