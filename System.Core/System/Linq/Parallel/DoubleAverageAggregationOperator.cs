using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A5 RID: 421
	internal sealed class DoubleAverageAggregationOperator : InlinedAggregationOperator<double, Pair<double, long>, double>
	{
		// Token: 0x06000E8F RID: 3727 RVA: 0x00033ECF File Offset: 0x000320CF
		internal DoubleAverageAggregationOperator(IEnumerable<double> child) : base(child)
		{
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00033ED8 File Offset: 0x000320D8
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double result;
			using (IEnumerator<Pair<double, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException(SR.GetString("NoElements"));
					result = 0.0;
				}
				else
				{
					Pair<double, long> pair = enumerator.Current;
					while (enumerator.MoveNext())
					{
						double first = pair.First;
						Pair<double, long> pair2 = enumerator.Current;
						pair.First = first + pair2.First;
						long second = pair.Second;
						pair2 = enumerator.Current;
						pair.Second = checked(second + pair2.Second);
					}
					result = pair.First / (double)pair.Second;
				}
			}
			return result;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00033F90 File Offset: 0x00032190
		protected override QueryOperatorEnumerator<Pair<double, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DoubleAverageAggregationOperator.DoubleAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003CD RID: 973
		private class DoubleAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<double, long>>
		{
			// Token: 0x06001DA7 RID: 7591 RVA: 0x0006A135 File Offset: 0x00068335
			internal DoubleAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<double, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DA8 RID: 7592 RVA: 0x0006A148 File Offset: 0x00068348
			protected override bool MoveNextCore(ref Pair<double, long> currentElement)
			{
				double num = 0.0;
				long num2 = 0L;
				QueryOperatorEnumerator<double, TKey> source = this.m_source;
				double num3 = 0.0;
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
						num += num3;
						checked
						{
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref num3, ref tkey));
					currentElement = new Pair<double, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x06001DA9 RID: 7593 RVA: 0x0006A1C6 File Offset: 0x000683C6
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001185 RID: 4485
			private QueryOperatorEnumerator<double, TKey> m_source;
		}
	}
}
