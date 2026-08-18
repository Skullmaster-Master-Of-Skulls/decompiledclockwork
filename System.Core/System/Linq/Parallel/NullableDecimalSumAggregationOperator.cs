using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B5 RID: 437
	internal sealed class NullableDecimalSumAggregationOperator : InlinedAggregationOperator<decimal?, decimal?, decimal?>
	{
		// Token: 0x06000EC4 RID: 3780 RVA: 0x00034A11 File Offset: 0x00032C11
		internal NullableDecimalSumAggregationOperator(IEnumerable<decimal?> child) : base(child)
		{
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00034A1C File Offset: 0x00032C1C
		protected override decimal? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal? result;
			using (IEnumerator<decimal?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				decimal num = 0.0m;
				while (enumerator.MoveNext())
				{
					decimal d = num;
					result = enumerator.Current;
					num = d + result.GetValueOrDefault();
				}
				result = new decimal?(num);
			}
			return result;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00034A88 File Offset: 0x00032C88
		protected override QueryOperatorEnumerator<decimal?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDecimalSumAggregationOperator.NullableDecimalSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003DB RID: 987
		private class NullableDecimalSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal?>
		{
			// Token: 0x06001DD1 RID: 7633 RVA: 0x0006AAA9 File Offset: 0x00068CA9
			internal NullableDecimalSumAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DD2 RID: 7634 RVA: 0x0006AABC File Offset: 0x00068CBC
			protected override bool MoveNextCore(ref decimal? currentElement)
			{
				decimal? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<decimal?, TKey> source = this.m_source;
				if (source.MoveNext(ref num, ref tkey))
				{
					decimal num2 = 0.0m;
					int num3 = 0;
					do
					{
						if ((num3++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						num2 += num.GetValueOrDefault();
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = new decimal?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06001DD3 RID: 7635 RVA: 0x0006AB3B File Offset: 0x00068D3B
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001198 RID: 4504
			private readonly QueryOperatorEnumerator<decimal?, TKey> m_source;
		}
	}
}
