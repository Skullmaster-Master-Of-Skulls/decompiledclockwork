using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D5 RID: 469
	internal sealed class GroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TOrderKey> : GroupByQueryOperatorEnumerator<TSource, TGroupKey, TSource, TOrderKey>
	{
		// Token: 0x06000F75 RID: 3957 RVA: 0x00036ADF File Offset: 0x00034CDF
		internal GroupByIdentityQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, IEqualityComparer<TGroupKey> keyComparer, CancellationToken cancellationToken) : base(source, keyComparer, cancellationToken)
		{
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00036AEC File Offset: 0x00034CEC
		protected override HashLookup<Wrapper<TGroupKey>, ListChunk<TSource>> BuildHashLookup()
		{
			HashLookup<Wrapper<TGroupKey>, ListChunk<TSource>> hashLookup = new HashLookup<Wrapper<TGroupKey>, ListChunk<TSource>>(new WrapperEqualityComparer<TGroupKey>(this.m_keyComparer));
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
				ListChunk<TSource> listChunk = null;
				if (!hashLookup.TryGetValue(key, ref listChunk))
				{
					listChunk = new ListChunk<TSource>(2);
					hashLookup.Add(key, listChunk);
				}
				listChunk.Add(pair.First);
			}
			return hashLookup;
		}
	}
}
