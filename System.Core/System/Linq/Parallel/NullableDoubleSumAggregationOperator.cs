using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B8 RID: 440
	internal sealed class NullableDoubleSumAggregationOperator : InlinedAggregationOperator<double?, double?, double?>
	{
		// Token: 0x06000ECD RID: 3789 RVA: 0x00034C95 File Offset: 0x00032E95
		internal NullableDoubleSumAggregationOperator(IEnumerable<double?> child) : base(child)
		{
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00034CA0 File Offset: 0x00032EA0
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double? result;
			using (IEnumerator<double?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = num;
					result = enumerator.Current;
					num = num2 + result.GetValueOrDefault();
				}
				result = new double?(num);
			}
			return result;
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00034D04 File Offset: 0x00032F04
		protected override QueryOperatorEnumerator<double?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDoubleSumAggregationOperator.NullableDoubleSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003DE RID: 990
		private class NullableDoubleSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double?>
		{
			// Token: 0x06001DDA RID: 7642 RVA: 0x0006AD4F File Offset: 0x00068F4F
			internal NullableDoubleSumAggregationOperatorEnumerator(QueryOperatorEnumerator<double?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DDB RID: 7643 RVA: 0x0006AD60 File Offset: 0x00068F60
			protected override bool MoveNextCore(ref double? currentElement)
			{
				double? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<double?, TKey> source = this.m_source;
				if (source.MoveNext(ref num, ref tkey))
				{
					double num2 = 0.0;
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
					currentElement = new double?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06001DDC RID: 7644 RVA: 0x0006ADD9 File Offset: 0x00068FD9
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400119C RID: 4508
			private readonly QueryOperatorEnumerator<double?, TKey> m_source;
		}
	}
}
