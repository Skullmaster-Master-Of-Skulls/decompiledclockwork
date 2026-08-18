using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B9 RID: 441
	internal sealed class NullableFloatAverageAggregationOperator : InlinedAggregationOperator<float?, Pair<double, long>, float?>
	{
		// Token: 0x06000ED0 RID: 3792 RVA: 0x00034D0F File Offset: 0x00032F0F
		internal NullableFloatAverageAggregationOperator(IEnumerable<float?> child) : base(child)
		{
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00034D18 File Offset: 0x00032F18
		protected override float? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float? num;
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
					num = new float?((float)(pair.First / (double)pair.Second));
				}
			}
			return num;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00034DC4 File Offset: 0x00032FC4
		protected override QueryOperatorEnumerator<Pair<double, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableFloatAverageAggregationOperator.NullableFloatAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003DF RID: 991
		private class NullableFloatAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<double, long>>
		{
			// Token: 0x06001DDD RID: 7645 RVA: 0x0006ADE6 File Offset: 0x00068FE6
			internal NullableFloatAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<float?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DDE RID: 7646 RVA: 0x0006ADF8 File Offset: 0x00068FF8
			protected override bool MoveNextCore(ref Pair<double, long> currentElement)
			{
				double num = 0.0;
				long num2 = 0L;
				QueryOperatorEnumerator<float?, TKey> source = this.m_source;
				float? num3 = null;
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
						num += (double)num3.GetValueOrDefault();
						num2 += 1L;
					}
				}
				currentElement = new Pair<double, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06001DDF RID: 7647 RVA: 0x0006AE7C File Offset: 0x0006907C
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400119D RID: 4509
			private QueryOperatorEnumerator<float?, TKey> m_source;
		}
	}
}
