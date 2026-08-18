using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B2 RID: 434
	internal sealed class LongSumAggregationOperator : InlinedAggregationOperator<long, long, long>
	{
		// Token: 0x06000EBB RID: 3771 RVA: 0x000347C5 File Offset: 0x000329C5
		internal LongSumAggregationOperator(IEnumerable<long> child) : base(child)
		{
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x000347D0 File Offset: 0x000329D0
		protected override long InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				long result;
				using (IEnumerator<long> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					long num = 0L;
					while (enumerator.MoveNext())
					{
						long num2 = enumerator.Current;
						num += num2;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00034820 File Offset: 0x00032A20
		protected override QueryOperatorEnumerator<long, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongSumAggregationOperator.LongSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003D8 RID: 984
		private class LongSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long>
		{
			// Token: 0x06001DC8 RID: 7624 RVA: 0x0006A84F File Offset: 0x00068A4F
			internal LongSumAggregationOperatorEnumerator(QueryOperatorEnumerator<long, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DC9 RID: 7625 RVA: 0x0006A860 File Offset: 0x00068A60
			protected override bool MoveNextCore(ref long currentElement)
			{
				long num = 0L;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<long, TKey> source = this.m_source;
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
							num2 += num;
						}
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06001DCA RID: 7626 RVA: 0x0006A8BE File Offset: 0x00068ABE
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001194 RID: 4500
			private readonly QueryOperatorEnumerator<long, TKey> m_source;
		}
	}
}
