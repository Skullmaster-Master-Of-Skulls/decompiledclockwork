using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A0 RID: 416
	internal sealed class CountAggregationOperator<TSource> : InlinedAggregationOperator<TSource, int, int>
	{
		// Token: 0x06000E80 RID: 3712 RVA: 0x00033BF3 File Offset: 0x00031DF3
		internal CountAggregationOperator(IEnumerable<TSource> child) : base(child)
		{
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00033BFC File Offset: 0x00031DFC
		protected override int InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				int result;
				using (IEnumerator<int> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					int num = 0;
					while (enumerator.MoveNext())
					{
						int num2 = enumerator.Current;
						num += num2;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00033C4C File Offset: 0x00031E4C
		protected override QueryOperatorEnumerator<int, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<TSource, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new CountAggregationOperator<TSource>.CountAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003C8 RID: 968
		private class CountAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int>
		{
			// Token: 0x06001D98 RID: 7576 RVA: 0x00069E1B File Offset: 0x0006801B
			internal CountAggregationOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001D99 RID: 7577 RVA: 0x00069E2C File Offset: 0x0006802C
			protected override bool MoveNextCore(ref int currentElement)
			{
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<TSource, TKey> source = this.m_source;
				if (source.MoveNext(ref tsource, ref tkey))
				{
					int num = 0;
					int num2 = 0;
					do
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						checked
						{
							num++;
						}
					}
					while (source.MoveNext(ref tsource, ref tkey));
					currentElement = num;
					return true;
				}
				return false;
			}

			// Token: 0x06001D9A RID: 7578 RVA: 0x00069E8E File Offset: 0x0006808E
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400117F RID: 4479
			private readonly QueryOperatorEnumerator<TSource, TKey> m_source;
		}
	}
}
