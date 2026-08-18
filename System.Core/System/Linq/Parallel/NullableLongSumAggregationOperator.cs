using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001C1 RID: 449
	internal sealed class NullableLongSumAggregationOperator : InlinedAggregationOperator<long?, long?, long?>
	{
		// Token: 0x06000EE8 RID: 3816 RVA: 0x000353A5 File Offset: 0x000335A5
		internal NullableLongSumAggregationOperator(IEnumerable<long?> child) : base(child)
		{
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x000353B0 File Offset: 0x000335B0
		protected override long? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			long? result;
			using (IEnumerator<long?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				long num = 0L;
				while (enumerator.MoveNext())
				{
					long num2 = num;
					result = enumerator.Current;
					num = checked(num2 + result.GetValueOrDefault());
				}
				result = new long?(num);
			}
			return result;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00035410 File Offset: 0x00033610
		protected override QueryOperatorEnumerator<long?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableLongSumAggregationOperator.NullableLongSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003E7 RID: 999
		private class NullableLongSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long?>
		{
			// Token: 0x06001DF5 RID: 7669 RVA: 0x0006B4A7 File Offset: 0x000696A7
			internal NullableLongSumAggregationOperatorEnumerator(QueryOperatorEnumerator<long?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DF6 RID: 7670 RVA: 0x0006B4B8 File Offset: 0x000696B8
			protected override bool MoveNextCore(ref long? currentElement)
			{
				long? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<long?, TKey> source = this.m_source;
				if (source.MoveNext(ref num, ref tkey))
				{
					long num2 = 0L;
					int num3 = 0;
					do
					{
						if ((num3++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						checked
						{
							num2 += num.GetValueOrDefault();
						}
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = new long?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06001DF7 RID: 7671 RVA: 0x0006B52A File Offset: 0x0006972A
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011A8 RID: 4520
			private readonly QueryOperatorEnumerator<long?, TKey> m_source;
		}
	}
}
