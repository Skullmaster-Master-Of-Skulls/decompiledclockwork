using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001AA RID: 426
	internal sealed class FloatSumAggregationOperator : InlinedAggregationOperator<float, double, float>
	{
		// Token: 0x06000E9E RID: 3742 RVA: 0x00034271 File Offset: 0x00032471
		internal FloatSumAggregationOperator(IEnumerable<float> child) : base(child)
		{
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003427C File Offset: 0x0003247C
		protected override float InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float result;
			using (IEnumerator<double> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = enumerator.Current;
					num += num2;
				}
				result = (float)num;
			}
			return result;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x000342D4 File Offset: 0x000324D4
		protected override QueryOperatorEnumerator<double, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new FloatSumAggregationOperator.FloatSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003D2 RID: 978
		private class FloatSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double>
		{
			// Token: 0x06001DB6 RID: 7606 RVA: 0x0006A4B1 File Offset: 0x000686B1
			internal FloatSumAggregationOperatorEnumerator(QueryOperatorEnumerator<float, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DB7 RID: 7607 RVA: 0x0006A4C4 File Offset: 0x000686C4
			protected override bool MoveNextCore(ref double currentElement)
			{
				float num = 0f;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<float, TKey> source = this.m_source;
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
						num2 += (double)num;
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06001DB8 RID: 7608 RVA: 0x0006A52D File Offset: 0x0006872D
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400118C RID: 4492
			private readonly QueryOperatorEnumerator<float, TKey> m_source;
		}
	}
}
