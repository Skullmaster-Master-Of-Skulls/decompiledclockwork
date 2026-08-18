using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B0 RID: 432
	internal sealed class LongAverageAggregationOperator : InlinedAggregationOperator<long, Pair<long, long>, double>
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003463B File Offset: 0x0003283B
		internal LongAverageAggregationOperator(IEnumerable<long> child) : base(child)
		{
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00034644 File Offset: 0x00032844
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				double result;
				using (IEnumerator<Pair<long, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					if (!enumerator.MoveNext())
					{
						singularExceptionToThrow = new InvalidOperationException(SR.GetString("NoElements"));
						result = 0.0;
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
						result = (double)pair.First / (double)pair.Second;
					}
				}
				return result;
			}
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x000346FC File Offset: 0x000328FC
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongAverageAggregationOperator.LongAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003D6 RID: 982
		private class LongAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x06001DC2 RID: 7618 RVA: 0x0006A701 File Offset: 0x00068901
			internal LongAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<long, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DC3 RID: 7619 RVA: 0x0006A714 File Offset: 0x00068914
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<long, TKey> source = this.m_source;
				long num3 = 0L;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref num3, ref tkey))
				{
					int num4 = 0;
					do
					{
						if ((num4++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						checked
						{
							num += num3;
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref num3, ref tkey));
					currentElement = new Pair<long, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x06001DC4 RID: 7620 RVA: 0x0006A784 File Offset: 0x00068984
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001191 RID: 4497
			private QueryOperatorEnumerator<long, TKey> m_source;
		}
	}
}
