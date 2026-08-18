using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D8 RID: 472
	internal sealed class OrderedGroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TOrderKey> : OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TSource, TOrderKey>
	{
		// Token: 0x06000F7D RID: 3965 RVA: 0x00036D06 File Offset: 0x00034F06
		internal OrderedGroupByIdentityQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, Func<TSource, TGroupKey> keySelector, IEqualityComparer<TGroupKey> keyComparer, IComparer<TOrderKey> orderComparer, CancellationToken cancellationToken) : base(source, keySelector, keyComparer, orderComparer, cancellationToken)
		{
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00036D18 File Offset: 0x00034F18
		protected override HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TSource, TOrderKey>.GroupKeyData> BuildHashLookup()
		{
			HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TSource, TOrderKey>.GroupKeyData> hashLookup = new HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TSource, TOrderKey>.GroupKeyData>(new WrapperEqualityComparer<TGroupKey>(this.m_keyComparer));
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
				OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TSource, TOrderKey>.GroupKeyData groupKeyData = null;
				if (hashLookup.TryGetValue(wrapper, ref groupKeyData))
				{
					if (this.m_orderComparer.Compare(torderKey, groupKeyData.m_orderKey) < 0)
					{
						groupKeyData.m_orderKey = torderKey;
					}
				}
				else
				{
					groupKeyData = new OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TSource, TOrderKey>.GroupKeyData(torderKey, wrapper.Value, this.m_orderComparer);
					hashLookup.Add(wrapper, groupKeyData);
				}
				groupKeyData.m_grouping.Add(pair.First, torderKey);
			}
			for (int i = 0; i < hashLookup.Count; i++)
			{
				hashLookup[i].Value.m_grouping.DoneAdding();
			}
			return hashLookup;
		}
	}
}
