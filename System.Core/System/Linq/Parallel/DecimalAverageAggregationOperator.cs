using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A2 RID: 418
	internal sealed class DecimalAverageAggregationOperator : InlinedAggregationOperator<decimal, Pair<decimal, long>, decimal>
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x00033CBB File Offset: 0x00031EBB
		internal DecimalAverageAggregationOperator(IEnumerable<decimal> child) : base(child)
		{
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00033CC4 File Offset: 0x00031EC4
		protected override decimal InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal result;
			using (IEnumerator<Pair<decimal, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException(SR.GetString("NoElements"));
					result = 0m;
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
					result = pair.First / pair.Second;
				}
			}
			return result;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00033D84 File Offset: 0x00031F84
		protected override QueryOperatorEnumerator<Pair<decimal, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DecimalAverageAggregationOperator.DecimalAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003CA RID: 970
		private class DecimalAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<decimal, long>>
		{
			// Token: 0x06001D9E RID: 7582 RVA: 0x00069F1D File Offset: 0x0006811D
			internal DecimalAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001D9F RID: 7583 RVA: 0x00069F30 File Offset: 0x00068130
			protected override bool MoveNextCore(ref Pair<decimal, long> currentElement)
			{
				decimal num = 0.0m;
				long num2 = 0L;
				QueryOperatorEnumerator<decimal, TKey> source = this.m_source;
				decimal d = 0m;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref d, ref tkey))
				{
					int num3 = 0;
					do
					{
						if ((num3++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						num += d;
						checked
						{
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref d, ref tkey));
					currentElement = new Pair<decimal, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x06001DA0 RID: 7584 RVA: 0x00069FB2 File Offset: 0x000681B2
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001181 RID: 4481
			private QueryOperatorEnumerator<decimal, TKey> m_source;
		}
	}
}
