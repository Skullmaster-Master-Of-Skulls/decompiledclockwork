using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A7 RID: 423
	internal sealed class DoubleSumAggregationOperator : InlinedAggregationOperator<double, double, double>
	{
		// Token: 0x06000E95 RID: 3733 RVA: 0x0003406D File Offset: 0x0003226D
		internal DoubleSumAggregationOperator(IEnumerable<double> child) : base(child)
		{
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00034078 File Offset: 0x00032278
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double result;
			using (IEnumerator<double> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = enumerator.Current;
					num += num2;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x000340D0 File Offset: 0x000322D0
		protected override QueryOperatorEnumerator<double, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DoubleSumAggregationOperator.DoubleSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003CF RID: 975
		private class DoubleSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double>
		{
			// Token: 0x06001DAD RID: 7597 RVA: 0x0006A2B1 File Offset: 0x000684B1
			internal DoubleSumAggregationOperatorEnumerator(QueryOperatorEnumerator<double, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DAE RID: 7598 RVA: 0x0006A2C4 File Offset: 0x000684C4
			protected override bool MoveNextCore(ref double currentElement)
			{
				double num = 0.0;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<double, TKey> source = this.m_source;
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
						num2 += num;
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06001DAF RID: 7599 RVA: 0x0006A330 File Offset: 0x00068530
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001188 RID: 4488
			private readonly QueryOperatorEnumerator<double, TKey> m_source;
		}
	}
}
