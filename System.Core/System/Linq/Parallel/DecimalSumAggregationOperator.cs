using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A4 RID: 420
	internal sealed class DecimalSumAggregationOperator : InlinedAggregationOperator<decimal, decimal, decimal>
	{
		// Token: 0x06000E8C RID: 3724 RVA: 0x00033E59 File Offset: 0x00032059
		internal DecimalSumAggregationOperator(IEnumerable<decimal> child) : base(child)
		{
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00033E64 File Offset: 0x00032064
		protected override decimal InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal result;
			using (IEnumerator<decimal> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				decimal num = 0.0m;
				while (enumerator.MoveNext())
				{
					decimal d = enumerator.Current;
					num += d;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00033EC4 File Offset: 0x000320C4
		protected override QueryOperatorEnumerator<decimal, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DecimalSumAggregationOperator.DecimalSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003CC RID: 972
		private class DecimalSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal>
		{
			// Token: 0x06001DA4 RID: 7588 RVA: 0x0006A0A1 File Offset: 0x000682A1
			internal DecimalSumAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DA5 RID: 7589 RVA: 0x0006A0B4 File Offset: 0x000682B4
			protected override bool MoveNextCore(ref decimal currentElement)
			{
				decimal d = 0m;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<decimal, TKey> source = this.m_source;
				if (source.MoveNext(ref d, ref tkey))
				{
					decimal num = 0.0m;
					int num2 = 0;
					do
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						num += d;
					}
					while (source.MoveNext(ref d, ref tkey));
					currentElement = num;
					return true;
				}
				return false;
			}

			// Token: 0x06001DA6 RID: 7590 RVA: 0x0006A128 File Offset: 0x00068328
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001184 RID: 4484
			private QueryOperatorEnumerator<decimal, TKey> m_source;
		}
	}
}
