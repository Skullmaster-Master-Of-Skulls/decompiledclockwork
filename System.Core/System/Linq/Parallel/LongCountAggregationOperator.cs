using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A1 RID: 417
	internal sealed class LongCountAggregationOperator<TSource> : InlinedAggregationOperator<TSource, long, long>
	{
		// Token: 0x06000E83 RID: 3715 RVA: 0x00033C57 File Offset: 0x00031E57
		internal LongCountAggregationOperator(IEnumerable<TSource> child) : base(child)
		{
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00033C60 File Offset: 0x00031E60
		protected override long InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				long result;
				using (IEnumerator<long> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					long num = 0L;
					while (enumerator.MoveNext())
					{
						long num2 = enumerator.Current;
						num += num2;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00033CB0 File Offset: 0x00031EB0
		protected override QueryOperatorEnumerator<long, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<TSource, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongCountAggregationOperator<TSource>.LongCountAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003C9 RID: 969
		private class LongCountAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long>
		{
			// Token: 0x06001D9B RID: 7579 RVA: 0x00069E9B File Offset: 0x0006809B
			internal LongCountAggregationOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001D9C RID: 7580 RVA: 0x00069EAC File Offset: 0x000680AC
			protected override bool MoveNextCore(ref long currentElement)
			{
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<TSource, TKey> source = this.m_source;
				if (source.MoveNext(ref tsource, ref tkey))
				{
					long num = 0L;
					int num2 = 0;
					do
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						checked
						{
							num += 1L;
						}
					}
					while (source.MoveNext(ref tsource, ref tkey));
					currentElement = num;
					return true;
				}
				return false;
			}

			// Token: 0x06001D9D RID: 7581 RVA: 0x00069F10 File Offset: 0x00068110
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001180 RID: 4480
			private readonly QueryOperatorEnumerator<TSource, TKey> m_source;
		}
	}
}
