using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001BC RID: 444
	internal sealed class NullableIntAverageAggregationOperator : InlinedAggregationOperator<int?, Pair<long, long>, double?>
	{
		// Token: 0x06000ED9 RID: 3801 RVA: 0x00034F8F File Offset: 0x0003318F
		internal NullableIntAverageAggregationOperator(IEnumerable<int?> child) : base(child)
		{
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00034F98 File Offset: 0x00033198
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

		// Token: 0x06000EDB RID: 3803 RVA: 0x00035044 File Offset: 0x00033244
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableIntAverageAggregationOperator.NullableIntAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003E2 RID: 994
		private class NullableIntAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x06001DE6 RID: 7654 RVA: 0x0006B07F File Offset: 0x0006927F
			internal NullableIntAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<int?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DE7 RID: 7655 RVA: 0x0006B090 File Offset: 0x00069290
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<int?, TKey> source = this.m_source;
				int? num3 = null;
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
						num += (long)num3.GetValueOrDefault();
						num2 += 1L;
					}
				}
				currentElement = new Pair<long, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06001DE8 RID: 7656 RVA: 0x0006B10D File Offset: 0x0006930D
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011A1 RID: 4513
			private QueryOperatorEnumerator<int?, TKey> m_source;
		}
	}
}
