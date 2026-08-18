using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B6 RID: 438
	internal sealed class NullableDoubleAverageAggregationOperator : InlinedAggregationOperator<double?, Pair<double, long>, double?>
	{
		// Token: 0x06000EC7 RID: 3783 RVA: 0x00034A93 File Offset: 0x00032C93
		internal NullableDoubleAverageAggregationOperator(IEnumerable<double?> child) : base(child)
		{
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00034A9C File Offset: 0x00032C9C
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double? num;
			using (IEnumerator<Pair<double, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
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
					num = new double?(pair.First / (double)pair.Second);
				}
			}
			return num;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00034B48 File Offset: 0x00032D48
		protected override QueryOperatorEnumerator<Pair<double, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDoubleAverageAggregationOperator.NullableDoubleAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003DC RID: 988
		private class NullableDoubleAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<double, long>>
		{
			// Token: 0x06001DD4 RID: 7636 RVA: 0x0006AB48 File Offset: 0x00068D48
			internal NullableDoubleAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<double?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DD5 RID: 7637 RVA: 0x0006AB5C File Offset: 0x00068D5C
			protected override bool MoveNextCore(ref Pair<double, long> currentElement)
			{
				double num = 0.0;
				long num2 = 0L;
				QueryOperatorEnumerator<double?, TKey> source = this.m_source;
				double? num3 = null;
				TKey tkey = default(TKey);
				int num4 = 0;
				while (source.MoveNext(ref num3, ref tkey))
				{
					if (num3 != null)
					{
						if ((num4++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						num += num3.GetValueOrDefault();
						num2 += 1L;
					}
				}
				currentElement = new Pair<double, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06001DD6 RID: 7638 RVA: 0x0006ABDF File Offset: 0x00068DDF
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001199 RID: 4505
			private QueryOperatorEnumerator<double?, TKey> m_source;
		}
	}
}
