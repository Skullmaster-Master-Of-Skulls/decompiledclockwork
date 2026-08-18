using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001AF RID: 431
	internal sealed class IntSumAggregationOperator : InlinedAggregationOperator<int, int, int>
	{
		// Token: 0x06000EB2 RID: 3762 RVA: 0x000345D5 File Offset: 0x000327D5
		internal IntSumAggregationOperator(IEnumerable<int> child) : base(child)
		{
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000345E0 File Offset: 0x000327E0
		protected override int InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				int result;
				using (IEnumerator<int> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					int num = 0;
					while (enumerator.MoveNext())
					{
						int num2 = enumerator.Current;
						num += num2;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00034630 File Offset: 0x00032830
		protected override QueryOperatorEnumerator<int, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new IntSumAggregationOperator.IntSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003D5 RID: 981
		private class IntSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int>
		{
			// Token: 0x06001DBF RID: 7615 RVA: 0x0006A685 File Offset: 0x00068885
			internal IntSumAggregationOperatorEnumerator(QueryOperatorEnumerator<int, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DC0 RID: 7616 RVA: 0x0006A698 File Offset: 0x00068898
			protected override bool MoveNextCore(ref int currentElement)
			{
				int num = 0;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<int, TKey> source = this.m_source;
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
							num2 += num;
						}
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06001DC1 RID: 7617 RVA: 0x0006A6F4 File Offset: 0x000688F4
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001190 RID: 4496
			private readonly QueryOperatorEnumerator<int, TKey> m_source;
		}
	}
}
