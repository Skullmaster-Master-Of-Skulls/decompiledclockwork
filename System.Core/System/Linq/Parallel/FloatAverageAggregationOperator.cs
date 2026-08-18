using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A8 RID: 424
	internal sealed class FloatAverageAggregationOperator : InlinedAggregationOperator<float, Pair<double, long>, float>
	{
		// Token: 0x06000E98 RID: 3736 RVA: 0x000340DB File Offset: 0x000322DB
		internal FloatAverageAggregationOperator(IEnumerable<float> child) : base(child)
		{
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x000340E4 File Offset: 0x000322E4
		protected override float InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float result;
			using (IEnumerator<Pair<double, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException(SR.GetString("NoElements"));
					result = 0f;
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
					result = (float)(pair.First / (double)pair.Second);
				}
			}
			return result;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00034198 File Offset: 0x00032398
		protected override QueryOperatorEnumerator<Pair<double, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new FloatAverageAggregationOperator.FloatAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003D0 RID: 976
		private class FloatAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<double, long>>
		{
			// Token: 0x06001DB0 RID: 7600 RVA: 0x0006A33D File Offset: 0x0006853D
			internal FloatAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<float, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DB1 RID: 7601 RVA: 0x0006A350 File Offset: 0x00068550
			protected override bool MoveNextCore(ref Pair<double, long> currentElement)
			{
				double num = 0.0;
				long num2 = 0L;
				QueryOperatorEnumerator<float, TKey> source = this.m_source;
				float num3 = 0f;
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
						num += (double)num3;
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

			// Token: 0x06001DB2 RID: 7602 RVA: 0x0006A3CB File Offset: 0x000685CB
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001189 RID: 4489
			private QueryOperatorEnumerator<float, TKey> m_source;
		}
	}
}
