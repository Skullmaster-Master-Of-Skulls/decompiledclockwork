using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B3 RID: 435
	internal sealed class NullableDecimalAverageAggregationOperator : InlinedAggregationOperator<decimal?, Pair<decimal, long>, decimal?>
	{
		// Token: 0x06000EBE RID: 3774 RVA: 0x0003482B File Offset: 0x00032A2B
		internal NullableDecimalAverageAggregationOperator(IEnumerable<decimal?> child) : base(child)
		{
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00034834 File Offset: 0x00032A34
		protected override decimal? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal? num;
			using (IEnumerator<Pair<decimal, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					Pair<decimal, long> pair = enumerator.Current;
					while (enumerator.MoveNext())
					{
						decimal first = pair.First;
						Pair<decimal, long> pair2 = enumerator.Current;
						pair.First = first + pair2.First;
						long second = pair.Second;
						pair2 = enumerator.Current;
						pair.Second = checked(second + pair2.Second);
					}
					num = new decimal?(pair.First / pair.Second);
				}
			}
			return num;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x000348EC File Offset: 0x00032AEC
		protected override QueryOperatorEnumerator<Pair<decimal, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDecimalAverageAggregationOperator.NullableDecimalAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003D9 RID: 985
		private class NullableDecimalAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<decimal, long>>
		{
			// Token: 0x06001DCB RID: 7627 RVA: 0x0006A8CB File Offset: 0x00068ACB
			internal NullableDecimalAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DCC RID: 7628 RVA: 0x0006A8DC File Offset: 0x00068ADC
			protected override bool MoveNextCore(ref Pair<decimal, long> currentElement)
			{
				decimal num = 0.0m;
				long num2 = 0L;
				QueryOperatorEnumerator<decimal?, TKey> source = this.m_source;
				decimal? num3 = null;
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
				currentElement = new Pair<decimal, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06001DCD RID: 7629 RVA: 0x0006A965 File Offset: 0x00068B65
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001195 RID: 4501
			private QueryOperatorEnumerator<decimal?, TKey> m_source;
		}
	}
}
