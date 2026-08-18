using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001BE RID: 446
	internal sealed class NullableIntSumAggregationOperator : InlinedAggregationOperator<int?, int?, int?>
	{
		// Token: 0x06000EDF RID: 3807 RVA: 0x00035161 File Offset: 0x00033361
		internal NullableIntSumAggregationOperator(IEnumerable<int?> child) : base(child)
		{
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003516C File Offset: 0x0003336C
		protected override int? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			int? result;
			using (IEnumerator<int?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				int num = 0;
				while (enumerator.MoveNext())
				{
					int num2 = num;
					result = enumerator.Current;
					num = checked(num2 + result.GetValueOrDefault());
				}
				result = new int?(num);
			}
			return result;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000351C8 File Offset: 0x000333C8
		protected override QueryOperatorEnumerator<int?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableIntSumAggregationOperator.NullableIntSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003E4 RID: 996
		private class NullableIntSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int?>
		{
			// Token: 0x06001DEC RID: 7660 RVA: 0x0006B24B File Offset: 0x0006944B
			internal NullableIntSumAggregationOperatorEnumerator(QueryOperatorEnumerator<int?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DED RID: 7661 RVA: 0x0006B25C File Offset: 0x0006945C
			protected override bool MoveNextCore(ref int? currentElement)
			{
				int? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<int?, TKey> source = this.m_source;
				if (source.MoveNext(ref num, ref tkey))
				{
					int num2 = 0;
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
					currentElement = new int?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06001DEE RID: 7662 RVA: 0x0006B2CD File Offset: 0x000694CD
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011A4 RID: 4516
			private QueryOperatorEnumerator<int?, TKey> m_source;
		}
	}
}
