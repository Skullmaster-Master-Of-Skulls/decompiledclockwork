using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001BF RID: 447
	internal sealed class NullableLongAverageAggregationOperator : InlinedAggregationOperator<long?, Pair<long, long>, double?>
	{
		// Token: 0x06000EE2 RID: 3810 RVA: 0x000351D3 File Offset: 0x000333D3
		internal NullableLongAverageAggregationOperator(IEnumerable<long?> child) : base(child)
		{
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000351DC File Offset: 0x000333DC
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				double? num;
				using (IEnumerator<Pair<long, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					if (!enumerator.MoveNext())
					{
						num = null;
						num = num;
					}
					else
					{
						Pair<long, long> pair = enumerator.Current;
						while (enumerator.MoveNext())
						{
							long first = pair.First;
							Pair<long, long> pair2 = enumerator.Current;
							pair.First = first + pair2.First;
							long second = pair.Second;
							pair2 = enumerator.Current;
							pair.Second = second + pair2.Second;
						}
						num = new double?((double)pair.First / (double)pair.Second);
					}
				}
				return num;
			}
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00035288 File Offset: 0x00033488
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableLongAverageAggregationOperator.NullableLongAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003E5 RID: 997
		private class NullableLongAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x06001DEF RID: 7663 RVA: 0x0006B2DA File Offset: 0x000694DA
			internal NullableLongAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<long?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DF0 RID: 7664 RVA: 0x0006B2EC File Offset: 0x000694EC
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<long?, TKey> source = this.m_source;
				long? num3 = null;
				TKey tkey = default(TKey);
				int num4 = 0;
				while (source.MoveNext(ref num3, ref tkey))
				{
					if ((num4++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this.m_cancellationToken);
					}
					if (num3 != null)
					{
						num += num3.GetValueOrDefault();
						num2 += 1L;
					}
				}
				currentElement = new Pair<long, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06001DF1 RID: 7665 RVA: 0x0006B368 File Offset: 0x00069568
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011A5 RID: 4517
			private QueryOperatorEnumerator<long?, TKey> m_source;
		}
	}
}
