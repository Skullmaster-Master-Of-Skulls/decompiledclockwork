using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001AD RID: 429
	internal sealed class IntAverageAggregationOperator : InlinedAggregationOperator<int, Pair<long, long>, double>
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x0003444C File Offset: 0x0003264C
		internal IntAverageAggregationOperator(IEnumerable<int> child) : base(child)
		{
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00034458 File Offset: 0x00032658
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

		// Token: 0x06000EAE RID: 3758 RVA: 0x00034510 File Offset: 0x00032710
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new IntAverageAggregationOperator.IntAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003D3 RID: 979
		private class IntAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x06001DB9 RID: 7609 RVA: 0x0006A53A File Offset: 0x0006873A
			internal IntAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<int, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DBA RID: 7610 RVA: 0x0006A54C File Offset: 0x0006874C
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<int, TKey> source = this.m_source;
				int num3 = 0;
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
							num += unchecked((long)num3);
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref num3, ref tkey));
					currentElement = new Pair<long, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x06001DBB RID: 7611 RVA: 0x0006A5BC File Offset: 0x000687BC
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400118D RID: 4493
			private QueryOperatorEnumerator<int, TKey> m_source;
		}
	}
}
